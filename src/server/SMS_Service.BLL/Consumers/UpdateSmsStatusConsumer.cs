using MassTransit;
using Microsoft.EntityFrameworkCore;
using SMS_Service.BLL.Exceptions;
using SMS_Service.Common.Messages;
using SMS_Service.DAL.Infrastructure;

namespace SMS_Service.BLL.Consumers
{
	public class UpdateSmsStatusConsumer : IConsumer<UpdateStatus>
	{
		private readonly IApplicationContext _applicationContext;

		public UpdateSmsStatusConsumer(IApplicationContext applicationContext)
		{
			_applicationContext = applicationContext;
		}

		public async Task Consume(ConsumeContext<UpdateStatus> context)
		{
			var sms = await _applicationContext.SMSs
				.Include(x => x.Receivers)
				.SingleOrDefaultAsync(x => x.Id == context.Message.SmsId);

			if (sms == null)
				throw new NotFoundException($"Sms with given id: {context.Message.SmsId} not found");

			sms.Receivers.Single(x => x.ReceiverNumber == context.Message.PhoneNumber).DeliveryStatus = context.Message.Status;

			await _applicationContext.SaveChangesAsync();
		}
	}
}
