using AutoMapper;
using SMS_Service.BLL.DataContracts;
using SMS_Service.DAL.Entities;

namespace SMS_Service.BLL.Mappings
{
	public class ReceiverProfile : Profile
	{
		public ReceiverProfile()
		{
			CreateMap<Receiver, ReceiverInfo>();
		}
	}
}
