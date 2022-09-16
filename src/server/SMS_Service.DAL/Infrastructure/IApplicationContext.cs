using Microsoft.EntityFrameworkCore;
using SMS_Service.DAL.Entities;

namespace SMS_Service.DAL.Infrastructure
{
	public interface IApplicationContext
	{
		DbSet<SMS> SMSs { get; }

		DbSet<Receiver> Receivers { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
