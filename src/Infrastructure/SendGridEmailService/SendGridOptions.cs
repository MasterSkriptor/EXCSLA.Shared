namespace EXCSLA.Shared.Infrastructure;

/// <summary>
/// Configuration options for the SendGrid email service.
/// </summary>
/// <remarks>
/// <para>Contains all required configuration for connecting to SendGrid API and setting email addresses.</para>
/// <para>All properties are required and must not be null or whitespace.</para>
/// </remarks>
public class SendGridOptions
{
    /// <summary>
    /// Gets or sets the SendGrid API key.
    /// </summary>
    /// <remarks>
    /// <para>Must be a valid SendGrid API key in format: SG.xxxxx</para>
    /// <para>Obtain from SendGrid dashboard: https://app.sendgrid.com/settings/api_keys</para>
    /// <para>Should be stored securely in environment variables or Azure Key Vault, never in source code.</para>
    /// </remarks>
    public required string Key { get; set; }

    /// <summary>
    /// Gets or sets the email address to send from.
    /// </summary>
    /// <remarks>
    /// <para>This is the sender address that appears as 'From' in email clients.</para>
    /// <para>Must be a verified sender address in your SendGrid account.</para>
    /// <para>Example: noreply@example.com</para>
    /// </remarks>
    public required string SendFromEmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the default reply-to email address.
    /// </summary>
    /// <remarks>
    /// <para>Used as the Reply-To header when no custom reply-to is specified.</para>
    /// <para>Recipients clicking 'Reply' in their email client will reply to this address.</para>
    /// <para>Example: support@example.com</para>
    /// </remarks>
    public required string ReplyToEmailAddress { get; set; }
}
