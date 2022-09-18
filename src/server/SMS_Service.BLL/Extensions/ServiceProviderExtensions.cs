using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SMS_Service.DAL;

namespace SMS_Service.BLL.Extensions
{
	public static class ServiceProviderExtensions
	{
		public static void ApplyMigrations(this IServiceProvider serviceProvider)
		{
			using (var scope = serviceProvider.CreateScope())
			{
				var services = scope.ServiceProvider;

				var context = services.GetRequiredService<ApplicationContext>();
				if (context.Database.GetPendingMigrations().Any())
				{
					context.Database.Migrate();
				}
			}
		}
	}
}
