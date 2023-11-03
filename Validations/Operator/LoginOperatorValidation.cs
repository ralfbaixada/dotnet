using Application.ViewModels;
using FluentValidation;
using Localization;
using Validations.Bases;

namespace Validations.Operator
{
    public class LoginOperatorValidation : AbstractValidator<LoginOperatorRequest>
    {
        public LoginOperatorValidation(Resources resources)
        {
            RuleFor(x => x.Email)
                .ApplyEmailValidation(resources);

            RuleFor(x => x.Password)
                .ApplyPasswordValidation(resources);
        }
    }
}
