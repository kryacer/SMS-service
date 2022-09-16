using MediatR;
using SMS_Service.BLL.DataContracts;

namespace SMS_Service.BLL.Commands
{
	public class SendSmsCommand : IRequest<SmsDto>
	{
		public string From { get; set; }

		public string[] To { get; set; }

		public string Content { get; set; }
	}
}
