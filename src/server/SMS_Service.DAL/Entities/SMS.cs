using SMS_Service.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS_Service.DAL.Entities
{
	[Table("SMSs")]
	public class SMS
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string From { get; set; }

        [Required]
        public string Content { get; set; }

		public ICollection<Receiver> Receivers { get; set; }
	}
}
