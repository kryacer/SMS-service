using MediatR;
using Microsoft.AspNetCore.Mvc;
using SMS_Service.BLL.Commands;
using SMS_Service.BLL.DataContracts;
using SMS_Service.BLL.Exceptions;
using SMS_Service.BLL.Queries;

namespace SMS_Service.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SMSController : ControllerBase
	{
		private readonly IMediator _mediator;

		public SMSController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> SendAsync([FromBody] SendSmsCommand command, CancellationToken cancellationToken)
		{
			var sms = await _mediator.Send(command, cancellationToken);

			return CreatedAtAction(nameof(GetByIdAsync), sms.Id, sms);
		}

		[HttpGet("{id}")]
		[ProducesDefaultResponseType(typeof(SmsDto))]
		[ProducesResponseType(typeof(ErrorDetail), StatusCodes.Status404NotFound)]
		public async Task<SmsDto> GetByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
		{
			return await _mediator.Send(new GetSmsByIdQuery(id), cancellationToken);
		}

		[HttpGet]
		[ProducesDefaultResponseType(typeof(SmsListDto))]
		public async Task<SmsListDto> GetAll([FromQuery] GetAllSmsQuery query, CancellationToken cancellationToken)
		{
			return await _mediator.Send(query, cancellationToken);
		}
	}
}
