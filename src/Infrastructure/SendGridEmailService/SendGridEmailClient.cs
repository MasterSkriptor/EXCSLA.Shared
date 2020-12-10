using SendGrid;
using SendGrid.Helpers.Mail;
using EXCSLA.Shared.Core.Interfaces;
using System.Threading.Tasks;
using Ardalis.GuardClauses;

namespace EXCSLA.Shared.Infrastructure
{
    public class SendGridEmailClient : IEmailSender
    {
        private readonly string _tempApiKey;
        private readonly string _sendFromEmailAccount;
        private readonly string _replyToEmailAccount;

        public SendGridEmailClient(SendGridOptions options)
        {
            Guard.Against.NullOrWhiteSpace(options.Key, nameof(options.Key));
            Guard.Against.NullOrWhiteSpace(options.SendFromEmailAddress, nameof(options.SendFromEmailAddress));
            Guard.Against.NullOrWhiteSpace(options.ReplyToEmailAddress, nameof(options.ReplyToEmailAddress));

            _tempApiKey = options.Key;
            _sendFromEmailAccount = options.SendFromEmailAddress;
            _replyToEmailAccount = options.ReplyToEmailAddress;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await SendEmailAsync(email, subject, message, _replyToEmailAccount);
        }

        public async Task SendEmailAsync(string email, string subject, string message, string from)
        {
            var client = new SendGridClient(_tempApiKey);
            var msg = new SendGridMessage();

            msg.SetFrom(_sendFromEmailAccount);
            msg.AddTo(email);
            msg.SetReplyTo(new EmailAddress(from));
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Html, message);

            await client.SendEmailAsync(msg);
        }
    }
}
