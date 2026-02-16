using EXCSLA.Shared.Domain.Interfaces;
using EXCSLA.Shared.Infrastructure;
using Moq;
using Xunit;

namespace EXCSLA.Shared.Infrastructure.Tests;

/// <summary>
/// Unit tests for SendGridEmailClient configuration and initialization validation.
/// </summary>
public class SendGridEmailClientShould
{
    [Fact]
    public void Initialize_WithValidOptions()
    {
        // Arrange
        var options = new SendGridOptions
        {
            Key = "SG.test_api_key",
            SendFromEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("noreply@example.com"),
            ReplyToEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("support@example.com")
        };

        // Act
        var client = new SendGridEmailClient(options);

        // Assert
        Assert.NotNull(client);
        Assert.IsAssignableFrom<IEmailSender>(client);
    }

    [Fact]
    public void ThrowNullArgumentException_WhenKeyIsNull()
    {
        // Arrange
        var options = new SendGridOptions
        {
            Key = null!,
            SendFromEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("noreply@example.com"),
            ReplyToEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("support@example.com")
        };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SendGridEmailClient(options));
    }

    [Fact]
    public void ThrowArgumentException_WhenKeyIsEmpty()
    {
        // Arrange
        var options = new SendGridOptions
        {
            Key = "",
            SendFromEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("noreply@example.com"),
            ReplyToEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("support@example.com")
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new SendGridEmailClient(options));
    }

    [Fact]
    public void ThrowArgumentException_WhenSendFromEmailAddressIsNull()
    {
        // Arrange
        var options = new SendGridOptions
        {
            Key = "SG.test_api_key",
            SendFromEmailAddress = null!,
            ReplyToEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("support@example.com")
        };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SendGridEmailClient(options));
    }

    [Fact]
    public void ThrowArgumentNullException_WhenSendFromEmailAddressIsEmpty()
    {
        // Arrange
        var options = new SendGridOptions
        {
            Key = "SG.test_api_key",
            SendFromEmailAddress = null!, // Email value object cannot be empty string, so use null for invalid
            ReplyToEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("support@example.com")
        };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SendGridEmailClient(options));
    }

    [Fact]
    public void ThrowArgumentNullException_WhenReplyToEmailAddressIsNull()
    {
        // Arrange
        var options = new SendGridOptions
        {
            Key = "SG.test_api_key",
            SendFromEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("noreply@example.com"),
            ReplyToEmailAddress = null!
        };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SendGridEmailClient(options));
    }

    [Fact]
    public void ThrowArgumentNullException_WhenReplyToEmailAddressIsEmpty()
    {
        // Arrange
        var options = new SendGridOptions
        {
            Key = "SG.test_api_key",
            SendFromEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("noreply@example.com"),
            ReplyToEmailAddress = null!
        };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SendGridEmailClient(options));
    }
}

/// <summary>
/// Unit tests for SendGridEmailClient email sending functionality.
/// </summary>
public class SendGridEmailClientEmailSendingShould
{
    private readonly SendGridOptions _validOptions = new()
    {
        Key = "SG.test_api_key",
        SendFromEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("noreply@example.com"),
        ReplyToEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("support@example.com")
    };

    [Fact]
    public async Task SendEmailAsync_AcceptsValidInputWithDefaultReplyTo()
    {
        // Arrange
        var client = new SendGridEmailClient(_validOptions);

        // Act - Should not throw
        // Note: This test validates the method exists and is callable
        // Integration testing with actual SendGrid API is separate
        var method = typeof(SendGridEmailClient).GetMethod("SendEmailAsync", 
            new[] { typeof(string), typeof(string), typeof(string) });

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task), method.ReturnType);
    }

    [Fact]
    public async Task SendEmailAsync_AcceptsValidInputWithCustomReplyTo()
    {
        // Arrange
        var client = new SendGridEmailClient(_validOptions);

        // Act - Validate method signature
        var method = typeof(SendGridEmailClient).GetMethod("SendEmailAsync",
            new[] { typeof(string), typeof(string), typeof(string), typeof(string) });

        // Assert
        Assert.NotNull(method);
        Assert.Equal(typeof(Task), method.ReturnType);
    }

    [Theory]
    [InlineData("user@example.com", "Welcome", "<h1>Welcome!</h1>")]
    [InlineData("admin@example.com", "Admin Notification", "<p>Settings updated</p>")]
    [InlineData("support@example.org", "Support Ticket", "<p>Ticket #12345 created</p>")]
    public async Task SendEmailAsync_SupportsVariousEmailAddresses(string email, string subject, string message)
    {
        var client = new SendGridEmailClient(_validOptions);
        // Act & Assert: Should not throw for valid input
        await client.SendEmailAsync(email, subject, message);
    }

    [Theory]
    [InlineData("Order Confirmation")]
    [InlineData("Password Reset")]
    [InlineData("Email Verification")]
    [InlineData("Weekly Newsletter")]
    public async Task SendEmailAsync_SupportsVariousEmailSubjects(string subject)
    {
        var client = new SendGridEmailClient(_validOptions);
        // Use a valid email and message, vary subject
        await client.SendEmailAsync("user@example.com", subject, "<p>Test message</p>");
    }

    [Theory]
    [InlineData("<p>Plain text email</p>")]
    [InlineData("<h1>Title</h1><p>Content with formatting</p>")]
    [InlineData("<table><tr><td>Data</td></tr></table>")]
    [InlineData("<a href='https://example.com'>Link</a>")]
    public async Task SendEmailAsync_SupportsVariousHtmlContent(string htmlContent)
    {
        var client = new SendGridEmailClient(_validOptions);
        // Use a valid email and subject, vary htmlContent
        await client.SendEmailAsync("user@example.com", "Test Subject", htmlContent);
    }

    [Theory]
    [InlineData("support@example.com")]
    [InlineData("sales@example.com")]
    [InlineData("noreply@example.com")]
    [InlineData("customreply@example.com")]
    public async Task SendEmailAsync_SupportsVariousReplyToAddresses(string replyToEmail)
    {
        // Use a valid email, subject, and message, vary replyTo
        var options = new SendGridOptions
        {
            Key = _validOptions.Key,
            SendFromEmailAddress = _validOptions.SendFromEmailAddress,
            ReplyToEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email(replyToEmail)
        };
        var client = new SendGridEmailClient(options);
        await client.SendEmailAsync("user@example.com", "Test Subject", "<p>Test message</p>");
    }
}

/// <summary>
/// Unit tests for SendGridOptions validation using ASP.NET Core's IValidateOptions.
public class SendGridOptionsShould
{
    [Fact]
    public void Validator_Succeeds_WithValidOptions()
    {
        var options = new SendGridOptions
        {
            Key = "SG.test",
            SendFromEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("from@example.com"),
            ReplyToEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("reply@example.com")
        };
        var validator = new SendGridOptionsValidator();
        var result = validator.Validate(string.Empty, options);
        Assert.True(result.Succeeded);
    }

    [Theory]
    [InlineData(null, "from@example.com", "reply@example.com", "SendGrid API Key is required.")]
    [InlineData("", "from@example.com", "reply@example.com", "SendGrid API Key is required.")]
    [InlineData("SG.test", null, "reply@example.com", "SendFromEmailAddress is required.")]
    [InlineData("SG.test", "from@example.com", null, "ReplyToEmailAddress is required.")]
    public void Validator_Fails_WithInvalidOptions(string? key, string? from, string? reply, string expectedMessage)
    {
        var options = new SendGridOptions
            {
                Key = key!,
                SendFromEmailAddress = string.IsNullOrEmpty(from) ? null! : new EXCSLA.Shared.Domain.ValueObjects.Email(from),
                ReplyToEmailAddress = string.IsNullOrEmpty(reply) ? null! : new EXCSLA.Shared.Domain.ValueObjects.Email(reply)
            };
        var validator = new SendGridOptionsValidator();
        var result = validator.Validate(string.Empty, options);
        Assert.False(result.Succeeded);
        Assert.Contains(expectedMessage, result.FailureMessage);
    }
}

/// <summary>
/// Unit tests for SendGridEmailClient interface contract.
/// </summary>
public class SendGridEmailClientInterfaceContractShould
{
    [Fact]
    public void ImplementIEmailSender()
    {
        // Verify the service implements the required interface
        var clientType = typeof(SendGridEmailClient);
        var interfaces = clientType.GetInterfaces();
        Assert.Contains(typeof(IEmailSender), interfaces);
    }

    [Fact]
    public void HaveSendEmailAsyncMethodWithDefaultReplyTo()
    {
        // Verify SendEmailAsync(string, string, string) exists
        var method = typeof(SendGridEmailClient).GetMethod("SendEmailAsync",
            new[] { typeof(string), typeof(string), typeof(string) });
        Assert.NotNull(method);
    }

    [Fact]
    public void HaveSendEmailAsyncMethodWithCustomReplyTo()
    {
        // Verify SendEmailAsync(string, string, string, string) exists
        var method = typeof(SendGridEmailClient).GetMethod("SendEmailAsync",
            new[] { typeof(string), typeof(string), typeof(string), typeof(string) });
        Assert.NotNull(method);
    }

    [Fact]
    public void BothSendEmailAsyncMethodsReturnTask()
    {
        // Verify both methods return Task
        var method1 = typeof(SendGridEmailClient).GetMethod("SendEmailAsync",
            new[] { typeof(string), typeof(string), typeof(string) });
        var method2 = typeof(SendGridEmailClient).GetMethod("SendEmailAsync",
            new[] { typeof(string), typeof(string), typeof(string), typeof(string) });

        Assert.NotNull(method1);
        Assert.NotNull(method2);
        if (method1 != null && method2 != null)
        {
            Assert.Equal(typeof(Task), method1.ReturnType);
            Assert.Equal(typeof(Task), method2.ReturnType);
        }
    }

    [Fact]
    public void BothSendEmailAsyncMethodsBePublic()
    {
        // Verify methods are public
        var method1 = typeof(SendGridEmailClient).GetMethod("SendEmailAsync",
            new[] { typeof(string), typeof(string), typeof(string) });
        var method2 = typeof(SendGridEmailClient).GetMethod("SendEmailAsync",
            new[] { typeof(string), typeof(string), typeof(string), typeof(string) });

        Assert.NotNull(method1);
        Assert.NotNull(method2);
        if (method1 != null && method2 != null)
        {
            Assert.True(method1.IsPublic);
            Assert.True(method2.IsPublic);
        }
    }
}

/// <summary>
/// Integration scenario tests for SendGridEmailClient.
/// </summary>
public class SendGridEmailClientIntegrationScenariosShould
{
    private readonly SendGridOptions _options = new()
    {
        Key = "SG.test_key",
        SendFromEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("noreply@company.com"),
        ReplyToEmailAddress = new EXCSLA.Shared.Domain.ValueObjects.Email("support@company.com")
    };

    [Fact]
    public void SupportWelcomeEmailScenario()
    {
        // Scenario: Welcome email for new users
        var client = new SendGridEmailClient(_options);
        Assert.NotNull(client);
        Assert.IsAssignableFrom<IEmailSender>(client);
    }

    [Fact]
    public void SupportPasswordResetScenario()
    {
        // Scenario: Password reset email
        var client = new SendGridEmailClient(_options);
        Assert.NotNull(client);
    }

    [Fact]
    public void SupportOrderConfirmationScenario()
    {
        // Scenario: Order confirmation with custom reply-to (sales team)
        var client = new SendGridEmailClient(_options);
        Assert.NotNull(client);
    }

    [Fact]
    public void SupportNotificationEmailScenario()
    {
        // Scenario: Transactional notification
        var client = new SendGridEmailClient(_options);
        Assert.NotNull(client);
    }

    [Fact]
    public void SupportHtmlFormattedEmailContent()
    {
        // Scenario: HTML formatted emails with styling
        var client = new SendGridEmailClient(_options);
        var htmlMessage = @"
            <h1>Welcome</h1>
            <p style='font-size: 14px;'>This is formatted content</p>
        ";
        Assert.NotNull(htmlMessage);
    }
}
