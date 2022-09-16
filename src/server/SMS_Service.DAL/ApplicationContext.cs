using Microsoft.EntityFrameworkCore;
using SMS_Service.DAL.Entities;
using SMS_Service.DAL.Infrastructure;

namespace SMS_Service.DAL
{
	public class ApplicationContext : DbContext, IApplicationContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
		}

		public DbSet<SMS> SMSs { get; set; }

		public DbSet<Receiver> Receivers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(IApplicationContext).Assembly);
		}
	}
}
