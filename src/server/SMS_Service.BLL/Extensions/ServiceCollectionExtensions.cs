using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMS_Service.BLL.AssemblyMarker;
using SMS_Service.BLL.Commands;
using SMS_Service.BLL.Consumers;
using SMS_Service.BLL.Queries;
using SMS_Service.BLL.Validators;
using SMS_Service.DAL;
using SMS_Service.DAL.Infrastructure;

namespace SMS_Service.BLL.Extensions
{
    public static class ServiceCollectionExtensions
	{
        public static void AddMediatR(this IServiceCollection services)
		{
            services.AddMediatR(typeof(IAssemblyMarker).Assembly);
		}

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(connectionString,
                    _ => _.MigrationsAssembly("SMS_Service.DAL"));
            });

            services.AddScoped<IApplicationContext, ApplicationContext>();

            return services;
        }

        public static IServiceCollection AddMassTransit(this IServiceCollection services)
		{
            services.AddMassTransit(x =>
            {
                x.AddConsumers(typeof(IAssemblyMarker).Assembly);
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((ctx, cfg) =>
                {

                    cfg.Host("host.docker.internal", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("update-status", e =>
                    {
                        e.ConfigureConsumer<UpdateSmsStatusConsumer>(ctx);
                    });
                });
            });

            return services;
        }

        public static void AddValidators(this IServiceCollection services)
		{
            services.AddScoped<IValidator<GetAllSmsQuery>, GetAllSmsQueryValidator>();
            services.AddScoped<IValidator<SendSmsCommand>, SendSmsCommandValidator>();
        }
    }
}
