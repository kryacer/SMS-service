using Microsoft.EntityFrameworkCore.Storage;
using SMS_Service.BLL.Exceptions;
using SMS_Service.DAL.Infrastructure;
using System.Data;
using System.Net;
using System.Text.Json;

namespace SMS_Service.API.Middlewares
{
	internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, 
            IWebHostEnvironment env, 
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext, IApplicationContext applicationContext)
        {
            IDbContextTransaction transaction = null;

            try
            {
                if(httpContext.Request.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
                    await _next(httpContext);
				else
				{
                    transaction = await applicationContext.Database.BeginTransactionAsync();

                    await _next(httpContext);

                    await transaction.CommitAsync();
                }
            }
            catch (Exception error)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                _logger.LogError(error, error.Message);

                var response = httpContext.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case FDXException e:
                        await WriteErrorAsync(
                            response,
                            e.StatusCode,
                            true,
                            new ErrorDetail
                            {
                                Code = e.ErrorCode,
                                Message = e.Message,
                                Error = e.ToString()
                            });
                        break;
                    default:
                        await WriteErrorAsync(
                            response,
                            HttpStatusCode.InternalServerError,
                            false,
                            new ErrorDetail
                            {
                                Code = ErrorCodes.InternalServerError,
                                Message = error.Message,
                                Error = error.ToString()
                            });
                        break;
                }
            }
        }

        private async Task WriteErrorAsync(HttpResponse response, HttpStatusCode httpStatusCode, bool force, params ErrorDetail[] errorDetails)
        {
            response.StatusCode = (int)httpStatusCode;

            var actualErrorDetails = errorDetails
                .Select(errorDetail => new ErrorDetail
                {
                    Code = errorDetail.Code,
                    Message = force || _env.IsDevelopment() ? errorDetail.Message : null,
                    Error = _env.IsDevelopment() ? errorDetail.Error : null
                })
                .ToArray();

            var serializationOption = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await JsonSerializer.SerializeAsync(response.Body, actualErrorDetails, serializationOption);
        }
    }
}
