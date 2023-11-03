using Application.ViewModels;
using FluentValidation;
using Localization;
using Validations.Bases;

namespace Validations.Operator
{
    public class CreateOperatorValidation : AbstractValidator<CreateOperatorViewModel>
    {
        public CreateOperatorValidation(Resources resources)
        {
            RuleFor(x => x.Name)
                .ApplyNameValidation(resources);

            RuleFor(x => x.Email)
                .ApplyEmailValidation(resources);

            RuleFor(x => x.Password)
                .ApplyPasswordValidation(resources);
        }
    }
}
