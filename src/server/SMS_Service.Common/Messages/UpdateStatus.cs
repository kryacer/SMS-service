using SMS_Service.Common.Enums;

namespace SMS_Service.Common.Messages
{
	public class UpdateStatus
	{
		public Guid SmsId { get; set; }

		public string PhoneNumber { get; set; }

		public SmsStatus Status { get; set; }
	}
}
