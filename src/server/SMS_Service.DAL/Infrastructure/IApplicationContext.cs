using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SMS_Service.DAL.Entities;

namespace SMS_Service.DAL.Infrastructure
{
	public interface IApplicationContext
	{
		DbSet<SMS> SMSs { get; }

		DbSet<Receiver> Receivers { get; }

		DatabaseFacade Database { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
