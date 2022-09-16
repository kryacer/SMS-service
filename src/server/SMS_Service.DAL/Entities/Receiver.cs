using System.ComponentModel.DataAnnotations.Schema;

namespace SMS_Service.DAL.Entities
{
	[Table("SmsToReceivers")]
	public class Receiver
	{
		[ForeignKey(nameof(SmsId))]
		public SMS SMS { get; set; }

		public Guid SmsId { get; set; }

		public string ReceiverNumber { get; set; }
	}
}
