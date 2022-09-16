using MassTransit;
using Microsoft.Extensions.Logging;
using SMS_Service.Common.Enums;
using SMS_Service.Common.Messages;
using System;
using System.Threading.Tasks;

namespace SMS_Service.Console.Consumers
{
	public class SendMessageConsumer : IConsumer<SendSms>
	{
		private readonly ILogger<SendMessageConsumer> _logger;
		private readonly IBus _bus;

		public SendMessageConsumer(ILogger<SendMessageConsumer> logger,
			IBus bus)
		{
			_logger = logger;
			_bus = bus;
		}
		public async Task Consume(ConsumeContext<SendSms> context)
		{
			Random random = new Random();
			var smsStatus = SmsStatus.None;

			if (random.Next(1, 3) > 1)
			{
				_logger.LogInformation($"Message delivered for {context.Message.To}");
				smsStatus = SmsStatus.Delivered;
			}
			else
			{
				_logger.LogError($"Sending failed for {context.Message.To}");
				smsStatus = SmsStatus.Failed;
			}

			var endpoint = await _bus.GetSendEndpoint(new Uri("queue:update-status"));
			await endpoint.Send(new UpdateStatus
			{
				SmsId = context.Message.Id, 
				PhoneNumber = context.Message.To, 
				Status = smsStatus 
			});
		}
	}
}
