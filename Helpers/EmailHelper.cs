using System.Net.Mail;
using System.Threading.Tasks;

namespace Helpers
{
    public static class EmailHelper
    {
        public static Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "myappteste123@hotmail.com";
            var pw = "Senha123@";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject: subject,
                                body: message
                                ));
        }
    }
}
