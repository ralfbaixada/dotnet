using Microsoft.Extensions.Localization;

namespace Localization
{
    public class Resources
    {
        private readonly IStringLocalizer<Resources> _localizer;

        public Resources(IStringLocalizer<Resources> localizer)
        {
            _localizer = localizer;
        }


        //public string FieldNotRequired(string field) => string.Format(_localizer[nameof(FieldNotRequired)], field);
        public string InvalidCharacters() => _localizer[nameof(InvalidCharacters)];
        public string RequireField() => _localizer[nameof(RequireField)];
        public string InvalidField() => _localizer[nameof(InvalidField)];
        public string EmailAlreadyExists() => _localizer[nameof(EmailAlreadyExists)];
        public string InvalidPassword() => _localizer[nameof(InvalidPassword)];
        public string InvalidCredentials() => _localizer[nameof(InvalidCredentials)];
        public string InvalidCodeResetPassword() => _localizer[nameof(InvalidCodeResetPassword)];
        public string InvalidCode() => _localizer[nameof(InvalidCode)];
        public string InvalidRole() => _localizer[nameof(InvalidRole)];
    }
}
