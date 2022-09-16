using SMS_Service.Common.Enums;

namespace SMS_Service.BLL.DataContracts
{
	public class SmsItemDto
	{
		public Guid Id { get; set; }

		public string From { get; set; }

		public string[] Receivers { get; set; }

		public SmsStatus Status { get; set; }
	}
}
