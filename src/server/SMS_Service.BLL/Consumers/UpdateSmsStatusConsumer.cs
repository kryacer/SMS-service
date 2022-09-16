using MassTransit;
using Microsoft.EntityFrameworkCore;
using SMS_Service.BLL.Exceptions;
using SMS_Service.Common.Messages;
using SMS_Service.DAL.Infrastructure;

namespace SMS_Service.BLL.Consumers
{
	internal class UpdateSmsStatusConsumer : IConsumer<UpdateStatus>
	{
		private readonly IApplicationContext _applicationContext;

		public UpdateSmsStatusConsumer(IApplicationContext applicationContext)
		{
			_applicationContext = applicationContext;
		}

		public async Task Consume(ConsumeContext<UpdateStatus> context)
		{
			var sms = await _applicationContext.SMSs.SingleOrDefaultAsync(x => x.Id == context.Message.SmsId);

			if (sms == null)
				throw new NotFoundException($"Sms with given id: {context.Message.SmsId} not found");

			sms.Status = context.Message.Status;

			await _applicationContext.SaveChangesAsync();
		}
	}
}
