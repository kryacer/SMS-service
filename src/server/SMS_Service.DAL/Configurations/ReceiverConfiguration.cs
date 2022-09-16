using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SMS_Service.DAL.Entities;

namespace SMS_Service.DAL.Configurations
{
	internal class ReceiverConfiguration : IEntityTypeConfiguration<Receiver>
    {
        public void Configure(EntityTypeBuilder<Receiver> builder)
        {
            builder.HasKey(x => new { x.SmsId, x.ReceiverNumber });
        }
    }
}
