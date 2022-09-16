using AutoMapper;
using SMS_Service.BLL.Commands;
using SMS_Service.BLL.DataContracts;
using SMS_Service.DAL.Entities;

namespace SMS_Service.BLL.Mappings
{
	public class SmsProfile : Profile
	{
		public SmsProfile()
		{
			CreateMap<SendSmsCommand, SMS>()
				.ForMember(d => d.Id, opt => opt.MapFrom(s => Guid.NewGuid()))
				.ForMember(d => d.Receivers, opt => opt.Ignore());

			CreateMap<SMS, SmsDto>();

			CreateMap<SMS, SmsItemDto>();
		}
	}
}
