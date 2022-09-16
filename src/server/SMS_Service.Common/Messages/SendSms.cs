namespace SMS_Service.Common.Messages
{
	public class SendSms
	{
		public Guid Id { get; set; }

		public string From { get; set; }

		public string To { get; set; }

		public string Content { get; set; }
	}
}
