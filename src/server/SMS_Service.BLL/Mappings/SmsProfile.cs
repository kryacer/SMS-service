using AutoMapper;
using SMS_Service.BLL.Commands;
using SMS_Service.BLL.DataContracts;
using SMS_Service.Common.Enums;
using SMS_Service.DAL.Entities;

namespace SMS_Service.BLL.Mappings
{
	public class SmsProfile : Profile
	{
		public SmsProfile()
		{
			CreateMap<SendSmsCommand, SMS>()
				.ForMember(d => d.Receivers, opt => opt.MapFrom(s => s.To.Select(number =>
					new Receiver { 
						ReceiverNumber = number,
						SmsId = Guid.NewGuid(),
					}
				)))
				.ForMember(d => d.Status, opt => opt.MapFrom(s => SmsStatus.Queued));

			CreateMap<SMS, SmsDto>()
				.ForMember(d => d.Receivers, opt => opt.MapFrom(s => s.Receivers.Select(r => r.ReceiverNumber)));

			CreateMap<SMS, SmsItemDto>()
				.ForMember(d => d.Receivers, opt => opt.MapFrom(s => s.Receivers.Select(r => r.ReceiverNumber)));
		}
	}
}
