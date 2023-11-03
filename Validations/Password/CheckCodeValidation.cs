using Application.ViewModels;
using FluentValidation;
using Localization;
using Validations.Bases;

namespace Validations.Password
{
    public class CheckCodeValidation : AbstractValidator<CheckCodeRequest>
    {
        public CheckCodeValidation(Resources resources)
        {
            RuleFor(x => x.Email)
                .ApplyEmailValidation(resources);

            RuleFor(x => x.Code)
                .ApplyCodeResetPasswordValidation(resources);
        }
    }
}
