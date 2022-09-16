using SMS_Service.Common.Enums;

namespace SMS_Service.BLL.DataContracts
{
	public class ReceiverInfo
	{
		public string ReceiverNumber { get; set; }

		public SmsStatus DeliveryStatus { get; set; }
	}
}
