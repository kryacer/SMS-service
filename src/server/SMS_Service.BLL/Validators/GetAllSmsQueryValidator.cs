using FluentValidation;
using SMS_Service.BLL.Extensions;
using SMS_Service.BLL.Queries;

namespace SMS_Service.BLL.Validators
{
	internal class GetAllSmsQueryValidator : AbstractValidator<GetAllSmsQuery>
	{
		public GetAllSmsQueryValidator()
		{
			RuleFor(x => x.PageNumber)
				.Required();
		}
	}
}
