using Application.ViewModels;
using FluentValidation;
using Localization;
using Validations.Bases;

namespace Validations.Password
{
    public class SendCodeResetPasswordValidation : AbstractValidator<SendCodeResetPasswordRequest>
    {
        public SendCodeResetPasswordValidation(Resources resources)
        {
            RuleFor(x => x.Email)
                .ApplyEmailValidation(resources);

            RuleFor(x => x.Code)
                .ApplyCodeResetPasswordValidation(resources);
        }
    }
}
