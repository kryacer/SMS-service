using FluentValidation;
using SMS_Service.BLL.Commands;
using SMS_Service.BLL.Extensions;

namespace SMS_Service.BLL.Validators
{
	public class SendSmsCommandValidator : AbstractValidator<SendSmsCommand>
	{
		public SendSmsCommandValidator()
		{
			RuleFor(x => x.From)
				.Cascade(CascadeMode.Stop)
				.Required();

			RuleFor(x => x.To)
				.Cascade(CascadeMode.Stop)
				.Required();

			RuleFor(x => x.Content)
				.Cascade(CascadeMode.Stop)
				.Required()
				.MaximumLength(140);
		}
	}
}
