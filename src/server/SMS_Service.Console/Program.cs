using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SMS_Service.Console.Consumers;
using System.Reflection;
using System.Threading.Tasks;

namespace SMS_Service.Console
{
	public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        var entryAssembly = Assembly.GetEntryAssembly();

                        x.AddConsumers(entryAssembly);
                        x.SetKebabCaseEndpointNameFormatter();

                        x.UsingRabbitMq((ctx, cfg) =>
						{
                            cfg.Host("host.docker.internal", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.ReceiveEndpoint("send-sms", e =>
                            {
                                e.ConfigureConsumer<SendMessageConsumer>(ctx);
                            });
                        });
                    });
                });
    }
}
