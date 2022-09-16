using MediatR;
using SMS_Service.BLL.DataContracts;

namespace SMS_Service.BLL.Queries
{
	public class GetAllSmsQuery : IRequest<SmsListDto>
	{
		public int PageNumber { get; set; }

		public int? PageSize { get; set; }
	}
}
