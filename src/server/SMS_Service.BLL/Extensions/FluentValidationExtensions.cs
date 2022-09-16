using FluentValidation;

namespace SMS_Service.BLL.Extensions
{
	internal static class FluentValidationExtensions
	{
		public static IRuleBuilderOptions<T, TProperty> Required<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
		{
			return ruleBuilder.NotNull().NotEmpty();
		}

		public static IRuleBuilderOptions<T, string> IsPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Matches(@"^[0][1-9]\d[1-9]$|^[1-9]\d{10}$").WithMessage("Please enter correct number");
		}
	}
}
