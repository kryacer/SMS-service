using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SMS_Service.BLL.DataContracts;
using SMS_Service.BLL.Extensions;
using SMS_Service.BLL.Queries;
using SMS_Service.DAL.Infrastructure;

namespace SMS_Service.BLL.Handlers
{
	public class GetAllSmsHandler : IRequestHandler<GetAllSmsQuery, SmsListDto>
	{
		private readonly IApplicationContext _applicationContext;
		private readonly IMapper _mapper;
		private readonly int _defaultPageSize;

		public GetAllSmsHandler(IApplicationContext applicationContext,
			IMapper mapper)
		{
			_applicationContext = applicationContext;
			_mapper = mapper;
			_defaultPageSize = 10;
		}

		public async Task<SmsListDto> Handle(GetAllSmsQuery request, CancellationToken cancellationToken)
		{
			var queryable = _applicationContext.SMSs.Include(x => x.Receivers).AsQueryable();

			var count = await queryable.CountAsync();

			queryable = queryable.Page(request.PageSize ?? _defaultPageSize, request.PageNumber);

			var list = await _mapper.ProjectTo<SmsItemDto>(queryable).ToArrayAsync(cancellationToken);

			return new SmsListDto
			{
				Total = count,
				List = list
			};
		}
	}
}
