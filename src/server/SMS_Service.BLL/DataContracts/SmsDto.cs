using SMS_Service.Common.Enums;

namespace SMS_Service.BLL.DataContracts
{
	public class SmsDto
	{
        public Guid Id { get; set; }

        public string From { get; set; }

        public string Content { get; set; }

        public ReceiverInfo[] Receivers { get; set; }
    }
}
