using FluentValidation;
using Helpers;
using Localization;

namespace Validations.Bases
{
    public static class BaseCode
    {
        public static IRuleBuilderOptions<T, string> ApplyCodeResetPasswordValidation<T>(this IRuleBuilderInitial<T, string> ruleBuilder, Resources resources)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage(resources.RequireField())
                .Must(code => code.Length != 6 || ValidationHelper.ValidCode(code) == false)
                .WithMessage(resources.InvalidCodeResetPassword());
        }
    }
}
