using MediatR;
using SMS_Service.BLL.DataContracts;

namespace SMS_Service.BLL.Queries
{
	public class GetSmsByIdQuery : IRequest<SmsDto>
	{
		public GetSmsByIdQuery(Guid id)
		{
			Id = id;
		}

		public Guid Id { get; set; }
	}
}
