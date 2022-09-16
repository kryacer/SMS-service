using FluentValidation;

namespace SMS_Service.BLL.Extensions
{
	internal static class FluentValidationExtensions
	{
		public static IRuleBuilderOptions<T, TProperty> Required<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
		{
			return ruleBuilder.NotNull().NotEmpty();
		}
	}
}
