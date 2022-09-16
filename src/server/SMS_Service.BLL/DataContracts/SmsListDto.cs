namespace SMS_Service.BLL.DataContracts
{
	public class SmsListDto
	{
		public SmsItemDto[] List { get; set; }

		public int Total { get; set; }
	}
}
