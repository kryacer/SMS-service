using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SMS_Service.BLL.DataContracts;
using SMS_Service.BLL.Exceptions;
using SMS_Service.BLL.Queries;
using SMS_Service.DAL.Infrastructure;

namespace SMS_Service.BLL.Handlers
{
	public class GetSmsByIdHandler : IRequestHandler<GetSmsByIdQuery, SmsDto>
	{
		private readonly IApplicationContext _applicationContext;
		private readonly IMapper _mapper;

		public GetSmsByIdHandler(IApplicationContext applicationContext, IMapper mapper)
		{
			_applicationContext = applicationContext;
			_mapper = mapper;
		}

		public async Task<SmsDto> Handle(GetSmsByIdQuery request, CancellationToken cancellationToken)
		{
			var sms = await _applicationContext.SMSs
				.Include(x => x.Receivers)
				.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

			if (sms == null)
				throw new NotFoundException($"Sms with given id: {request.Id} not found");

			return _mapper.Map<SmsDto>(sms);
		}
	}
}
