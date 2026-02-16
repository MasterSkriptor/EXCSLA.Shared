using SendGrid;
using SendGrid.Helpers.Mail;
using EXCSLA.Shared.Domain.Interfaces;
using Ardalis.GuardClauses;
using EXCSLA.Shared.Domain.ValueObjects;

namespace EXCSLA.Shared.Infrastructure;

/// <summary>
/// Email client implementation using SendGrid for sending transactional and marketing emails.
/// </summary>
/// <remarks>
/// <para>Provides a reliable wrapper around SendGrid's REST API for email delivery.</para>
/// <para>Features:</para>
/// <list type="bullet">
/// <item>Configurable sender and reply-to addresses</item>
/// <item>Support for HTML and plain text email content</item>
/// <item>Default and custom reply-to address handling</item>
/// <item>Automatic validation of required configuration</item>
/// <item>Async-first email sending operations</item>
/// </list>
/// <para>Configuration:</para>
/// <code>
/// var options = new SendGridOptions
/// {
///     Key = "SG.xxxxx",
///     SendFromEmailAddress = "noreply@example.com",
///     ReplyToEmailAddress = "support@example.com"
/// };
/// var emailClient = new SendGridEmailClient(options);
/// </code>
/// </remarks>
public class SendGridEmailClient : IEmailSender
{
    private readonly string _tempApiKey;
    private readonly Email _sendFromEmailAccount;
    private readonly Email _replyToEmailAccount;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendGridEmailClient"/> class.
    /// </summary>
    /// <param name="options">Configuration options containing SendGrid API key and email addresses.</param>
    /// <remarks>
    /// <para>Validates that all required options are provided:</para>
    /// <list type="bullet">
    /// <item>Key: SendGrid API key (must not be null or whitespace)</item>
    /// <item>SendFromEmailAddress: Email address to send from (must not be null or whitespace)</item>
    /// <item>ReplyToEmailAddress: Default reply-to address (must not be null or whitespace)</item>
    /// </list>
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown if any required option is null or whitespace.</exception>
    public SendGridEmailClient(SendGridOptions options)
    {
        Guard.Against.NullOrWhiteSpace(options.Key, nameof(options.Key));
        if (options.SendFromEmailAddress is null)
            throw new ArgumentNullException(nameof(options.SendFromEmailAddress));
        if (options.ReplyToEmailAddress is null)
            throw new ArgumentNullException(nameof(options.ReplyToEmailAddress));

        _tempApiKey = options.Key;
        _sendFromEmailAccount = options.SendFromEmailAddress;
        _replyToEmailAccount = options.ReplyToEmailAddress;
    }

    /// <summary>
    /// Sends an email asynchronously using the default reply-to address from configuration.
    /// </summary>
    /// <param name="email">The recipient email address.</param>
    /// <param name="subject">The email subject line.</param>
    /// <param name="message">The email body content in HTML or plain text format.</param>
    /// <remarks>
    /// <para>Uses the ReplyToEmailAddress configured during initialization.</para>
    /// <para>The email is sent from the configured SendFromEmailAddress.</para>
    /// <para>Content is formatted as HTML.</para>
    /// </remarks>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    /// <exception cref="SendGridClientException">Thrown if the SendGrid API request fails.</exception>
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        await SendEmailAsync(email, subject, message, _replyToEmailAccount.ToString());
    }

    /// <summary>
    /// Sends an email asynchronously with a custom reply-to address.
    /// </summary>
    /// <param name="email">The recipient email address.</param>
    /// <param name="subject">The email subject line.</param>
    /// <param name="message">The email body content in HTML or plain text format.</param>
    /// <param name="from">The custom reply-to email address for this specific email.</param>
    /// <remarks>
    /// <para>Allows overriding the default reply-to address on a per-email basis.</para>
    /// <para>The email is still sent from the configured SendFromEmailAddress.</para>
    /// <para>The 'from' parameter is used as the Reply-To header.</para>
    /// <para>Content is formatted as HTML.</para>
    /// </remarks>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    /// <exception cref="SendGridClientException">Thrown if the SendGrid API request fails.</exception>
    public async Task SendEmailAsync(string email, string subject, string message, string from)
    {
        var client = new SendGridClient(_tempApiKey);
        var msg = new SendGridMessage();

        msg.SetFrom(_sendFromEmailAccount.ToString());
        msg.AddTo(email);
        msg.SetReplyTo(new EmailAddress(from));
        msg.SetSubject(subject);
        msg.AddContent(MimeType.Html, message);

        await client.SendEmailAsync(msg);
    }
}
