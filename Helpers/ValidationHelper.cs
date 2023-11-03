using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Helpers
{
    public class ValidationHelper
    {
        public static readonly string _validCaracteresName = "^[^0-9_!¡÷+,?¿/\\+=@#$%ˆ&*(){}|~<>,;\"\\=:\\[\\]]*$";
        public static readonly string _validCaracteresPlate = @"^[A-Za-z]{3}\d{1}[A-Za-z0-9]{1}\d{2}$";
        public static readonly string _validCaracteresObservation = @"^[^<>""=]*$";

        public static bool CheckContainCharacters(string value, string caracteres)
        {
            return Regex.IsMatch(value, caracteres);
        }

        public static bool DateIsBiggerThan(DateTime? date, int years)
        {
            if (!date.HasValue)
                return false;

            var minimumDate = DateTime.Now.AddYears(years * -1);

            return date > minimumDate;
        }

        public static bool ValidAge(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                return false;

            var minimumDate = DateTime.Now.AddYears(-100).Date;
            var maximumDate = DateTime.Now.AddYears(-18).Date;

            return birthDate.Value.Date >= minimumDate && birthDate.Value.Date <= maximumDate;
        }

        public static bool ValidEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool ValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (password.Length < 8)
                return false;

            if (!Regex.IsMatch(password, @"[a-z]"))
                return false;

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            if (!Regex.IsMatch(password, @"[0-9]"))
                return false;

            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]"))
                return false;

            return true;
        }

        public static bool ValidCode(string code)
        {
            if (!Regex.IsMatch(code, @"[0-9]"))
                return false;

            return true;
        }
    }
}
