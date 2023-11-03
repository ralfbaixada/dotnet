using FluentValidation;
using Helpers;
using Localization;

namespace Validations.Bases
{
    public static class BaseOperatorValidation
    {
        public static IRuleBuilderOptions<T, string> ApplyNameValidation<T>(this IRuleBuilderInitial<T, string> ruleBuilder, Resources resources)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage(resources.RequireField())
                .Must(name => ValidationHelper.CheckContainCharacters(name, ValidationHelper._validCaracteresName))
                .WithMessage(resources.InvalidCharacters());
        }

        public static IRuleBuilderOptions<T, string> ApplyEmailValidation<T>(this IRuleBuilderInitial<T, string> ruleBuilder, Resources resources)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage(resources.RequireField())
                .Must(email => ValidationHelper.ValidEmail(email))
                .WithMessage(resources.InvalidField());
        }

        public static IRuleBuilderOptions<T, string> ApplyPasswordValidation<T>(this IRuleBuilderInitial<T, string> ruleBuilder, Resources resources)
        {
            return ruleBuilder
                .NotEmpty()
                .WithMessage(resources.RequireField())
                .Must(password => ValidationHelper.ValidPassword(password))
                .WithMessage(resources.InvalidPassword());
        }
    }
}
