using SMS_Service.BLL.Exceptions;
using System.Net;
using System.Text.Json;

namespace SMS_Service.API.Middlewares
{
	internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);

                var response = context.Response;
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
