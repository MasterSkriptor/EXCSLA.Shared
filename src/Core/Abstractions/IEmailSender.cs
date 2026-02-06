using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EXCSLA.Shared.Core.Interfaces;

/// <summary>
/// Interface for email sending functionality.
/// Abstracts email delivery to allow different implementations (SendGrid, SMTP, etc.).
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Sends an email asynchronously with default sender information.
    /// </summary>
    /// <param name="email">The recipient email address.</param>
    /// <param name="subject">The email subject line.</param>
    /// <param name="message">The email body content (HTML or plain text).</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendEmailAsync(string email, string subject, string message);

    /// <summary>
    /// Sends an email asynchronously with specified sender information.
    /// </summary>
    /// <param name="email">The recipient email address.</param>
    /// <param name="subject">The email subject line.</param>
    /// <param name="message">The email body content (HTML or plain text).</param>
    /// <param name="from">The sender email address.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// This overload allows specifying a custom sender address, useful for
    /// different notification types or departments.
    /// </remarks>
    Task SendEmailAsync(string email, string subject, string message, string from);
}
