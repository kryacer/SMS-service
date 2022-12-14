using AutoMapper;
using MassTransit;
using MediatR;
using SMS_Service.BLL.Commands;
using SMS_Service.BLL.DataContracts;
using SMS_Service.Common.Enums;
using SMS_Service.Common.Messages;
using SMS_Service.DAL.Entities;
using SMS_Service.DAL.Infrastructure;

namespace SMS_Service.BLL.Handlers
{
	internal class SendSmsHandler : IRequestHandler<SendSmsCommand, SmsDto>
	{
		private readonly IApplicationContext _applicationContext;
		private readonly IMapper _mapper;
		private readonly IBus _bus;

		public SendSmsHandler(IApplicationContext applicationContext, IMapper mapper, IBus bus)
		{
			_applicationContext = applicationContext;
			_mapper = mapper;
			_bus = bus;
		}

		public async Task<SmsDto> Handle(SendSmsCommand request, CancellationToken cancellationToken)
		{
			var sms = _mapper.Map<SMS>(request);

			sms.Receivers = request.To.Distinct().Select(t => new Receiver { 
				SmsId = sms.Id, 
				ReceiverNumber = t,
				DeliveryStatus = SmsStatus.Queued
			}).ToList();

			_applicationContext.SMSs.Add(sms);
			await _applicationContext.SaveChangesAsync(cancellationToken);

			var smsDto = _mapper.Map<SmsDto>(sms);

			await SendSmsAsync(smsDto, cancellationToken);

			return smsDto;
		}

		private async Task SendSmsAsync(SmsDto sms, CancellationToken cancellationToken)
		{
			var endpoint = await _bus.GetSendEndpoint(new Uri("queue:send-sms"));
			var sendingTasks = sms.Receivers.Select(receiverInfo => endpoint.Send(new SendSms
			{
				Id = sms.Id,
				From = sms.From,
				To = receiverInfo.ReceiverNumber
			}, cancellationToken));

			await Task.WhenAll(sendingTasks);
		}
	}
}
