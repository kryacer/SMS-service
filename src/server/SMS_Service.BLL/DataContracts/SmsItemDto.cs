namespace SMS_Service.BLL.DataContracts
{
	public class SmsItemDto
	{
		public Guid Id { get; set; }

		public string From { get; set; }

		public ReceiverInfo[] Receivers { get; set; }
	}
}
