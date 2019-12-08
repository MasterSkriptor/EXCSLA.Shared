using SendGrid;
using SendGrid.Helpers.Mail;
using EXCSLA.Shared.Core.Interfaces;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Infrastructure
{
    public class SendGridEmailClient : IEmailSender
    {
        private readonly string _tempApiKey;

        public SendGridEmailClient(SendGridOptions options)
        {
            _tempApiKey = options.Key;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await SendEmailAsync(email, subject, message, "he@excsla.com");
        }

        public async Task SendEmailAsync(string email, string subject, string message, string from)
        {
            var client = new SendGridClient(_tempApiKey);
            var msg = new SendGridMessage();

            msg.SetFrom("donotreply@mamatara.org");
            msg.AddTo(email);
            msg.SetReplyTo(new EmailAddress(from));
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Html, message);

            await client.SendEmailAsync(msg);
        }
    }
}
