using FluentValidation.AspNetCore;
using SMS_Service.API.Middlewares;
using SMS_Service.BLL.AssemblyMarker;
using SMS_Service.BLL.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidators();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddAutoMapper(typeof(IAssemblyMarker).Assembly);
builder.Services.AddMassTransit();
builder.Services.AddMediatR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
