using Microsoft.Extensions.Options;

namespace EXCSLA.Shared.Infrastructure;

/// <summary>
/// ASP.NET Core options validator for SendGridOptions.
/// </summary>
public class SendGridOptionsValidator : IValidateOptions<SendGridOptions>
{
    public ValidateOptionsResult Validate(string name, SendGridOptions options)
    {
        if (options == null)
            return ValidateOptionsResult.Fail("SendGridOptions configuration section is missing.");
        if (string.IsNullOrWhiteSpace(options.Key))
            return ValidateOptionsResult.Fail("SendGrid API Key is required.");
        if (options.SendFromEmailAddress is null)
            return ValidateOptionsResult.Fail("SendFromEmailAddress is required.");
        if (options.ReplyToEmailAddress is null)
            return ValidateOptionsResult.Fail("ReplyToEmailAddress is required.");
        // Optionally: Add regex/email format validation here
        return ValidateOptionsResult.Success;
    }
}
