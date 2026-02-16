# SendGrid Email Service

A production-ready wrapper around SendGrid's REST API for sending transactional and marketing emails with support for HTML content, custom reply-to addresses, and configurable sender information.

## Table of Contents

- [Overview](#overview)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [API Reference](#api-reference)
- [Best Practices](#best-practices)
- [Architecture](#architecture)
- [Common Scenarios](#common-scenarios)
- [Troubleshooting](#troubleshooting)

## Overview

The `SendGridEmailClient` provides a simplified interface to SendGrid that:

- **Manages** email composition and delivery via SendGrid API
- **Validates** required configuration (API key, sender, reply-to addresses)
- **Supports** both default and custom reply-to addresses
- **Formats** emails as HTML content for rich formatting
- **Implements** the `IEmailSender` interface for abstraction
- **Handles** asynchronous email delivery
- **Abstracts** SendGrid complexity for common use cases

## Installation

### Prerequisites

- .NET 10.0 or later
- SendGrid account with verified sender address
- SendGrid.CSharp NuGet package (v9.28.1+)

### NuGet Installation

```bash
dotnet add package SendGrid --version 9.28.1
```

The service is included in the `EXCSLA.Shared.Infrastructure.SendGridEmailService` package.

## Configuration

### Setup in Dependency Injection Container

Register the service in your `Program.cs` or dependency injection configuration:

```csharp
using EXCSLA.Shared.Infrastructure;
using EXCSLA.Shared.Domain.Interfaces;

// In your DI setup
var services = new ServiceCollection();

// Configure SendGrid options
var sendGridOptions = new SendGridOptions
{
    Key = "SG.xxxxxxxxxxxx",
    SendFromEmailAddress = "noreply@yourcompany.com",
    ReplyToEmailAddress = "support@yourcompany.com"
};

// Register the email client
services.AddSingleton(sendGridOptions);
services.AddSingleton<IEmailSender, SendGridEmailClient>();

var provider = services.BuildServiceProvider();
```

### Environment Configuration

Store your SendGrid API key securely in environment variables or configuration:

```csharp
// From environment variables
var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY")
    ?? throw new InvalidOperationException("SENDGRID_API_KEY not configured");

var sendFromEmail = Environment.GetEnvironmentVariable("SENDGRID_FROM_EMAIL")
    ?? throw new InvalidOperationException("SENDGRID_FROM_EMAIL not configured");

var replyToEmail = Environment.GetEnvironmentVariable("SENDGRID_REPLY_TO_EMAIL")
    ?? throw new InvalidOperationException("SENDGRID_REPLY_TO_EMAIL not configured");

var options = new SendGridOptions
{
    Key = apiKey,
    SendFromEmailAddress = sendFromEmail,
    ReplyToEmailAddress = replyToEmail
};

services.AddSingleton(options);
services.AddSingleton<IEmailSender, SendGridEmailClient>();
```

### Azure Key Vault Configuration

For production environments, use Azure Key Vault:

```csharp
var keyVaultUrl = new Uri(Environment.GetEnvironmentVariable("KEY_VAULT_URL")!);
var credential = new DefaultAzureCredential();
var client = new SecretClient(keyVaultUrl, credential);

var apiKeySecret = await client.GetSecretAsync("sendgrid-api-key");
var fromEmailSecret = await client.GetSecretAsync("sendgrid-from-email");
var replyToEmailSecret = await client.GetSecretAsync("sendgrid-reply-to-email");

var options = new SendGridOptions
{
    Key = apiKeySecret.Value.Value,
    SendFromEmailAddress = fromEmailSecret.Value.Value,
    ReplyToEmailAddress = replyToEmailSecret.Value.Value
};

services.AddSingleton(options);
services.AddSingleton<IEmailSender, SendGridEmailClient>();
```

## Usage

### Inject and Use the Service

```csharp
public class UserNotificationService
{
    private readonly IEmailSender _emailSender;

    public UserNotificationService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task SendWelcomeEmail(string userEmail, string userName)
    {
        var subject = "Welcome to Our Platform";
        var message = $@"
            <h1>Welcome, {userName}!</h1>
            <p>Thank you for signing up. We're excited to have you on board.</p>
            <p>Get started with our <a href='https://docs.example.com'>documentation</a>.</p>
        ";
        
        await _emailSender.SendEmailAsync(userEmail, subject, message);
    }
}
```

### Send Email with Default Reply-To Address

```csharp
// Uses the ReplyToEmailAddress configured during setup
var subject = "Password Reset Request";
var htmlMessage = @"
    <p>We received a password reset request for your account.</p>
    <p><a href='https://example.com/reset?token=abc123'>Reset your password</a></p>
    <p>This link expires in 24 hours.</p>
";

await _emailSender.SendEmailAsync("user@example.com", subject, htmlMessage);
```

### Send Email with Custom Reply-To Address

```csharp
// Override reply-to address for this specific email
var subject = "Order Confirmation";
var htmlMessage = @"
    <h2>Thank you for your order!</h2>
    <p>Order ID: #12345</p>
    <p>Total: $99.99</p>
    <p>Estimated delivery: 3-5 business days</p>
";

// Use sales team's email as reply-to instead of support
await _emailSender.SendEmailAsync(
    "customer@example.com", 
    subject, 
    htmlMessage, 
    "sales@example.com"
);
```

## API Reference

### IEmailSender Interface

The `SendGridEmailClient` implements the `IEmailSender` interface with two methods:

#### `SendEmailAsync(string email, string subject, string message)`

Sends an email using the default reply-to address.

**Parameters:**
- `email` (string) - Recipient email address
- `subject` (string) - Email subject line
- `message` (string) - Email body in HTML or plain text format

**Returns:** Task - A task representing the asynchronous operation

**Example:**
```csharp
await _emailSender.SendEmailAsync(
    "user@example.com",
    "Account Verification",
    "<p>Please verify your email address.</p>"
);
```

---

#### `SendEmailAsync(string email, string subject, string message, string from)`

Sends an email with a custom reply-to address.

**Parameters:**
- `email` (string) - Recipient email address
- `subject` (string) - Email subject line
- `message` (string) - Email body in HTML or plain text format
- `from` (string) - Custom reply-to email address

**Returns:** Task - A task representing the asynchronous operation

**Example:**
```csharp
await _emailSender.SendEmailAsync(
    "user@example.com",
    "Support Ticket Response",
    "<p>Your ticket has been updated.</p>",
    "support@example.com"
);
```

## Best Practices

### 1. HTML Email Formatting

Always use proper HTML for better email client compatibility:

```csharp
var htmlMessage = @"
    <html>
        <body style='font-family: Arial, sans-serif;'>
            <h1>Hello!</h1>
            <p>This is a <strong>bold</strong> statement.</p>
            <a href='https://example.com' style='color: #0069d9;'>Click here</a>
        </body>
    </html>
";

await _emailSender.SendEmailAsync(email, subject, htmlMessage);
```

### 2. Verified Sender Addresses

Ensure all sender and reply-to addresses are verified in SendGrid:

```
✅ Verified in SendGrid Dashboard
  - noreply@yourcompany.com
  - support@yourcompany.com
  - sales@yourcompany.com

❌ Not verified (will fail)
  - any-unverified-address@example.com
```

### 3. Configuration Management

Never hardcode credentials in source code:

```csharp
// ❌ WRONG
var options = new SendGridOptions
{
    Key = "SG.live_api_key_exposed_in_code",
    SendFromEmailAddress = "noreply@example.com",
    ReplyToEmailAddress = "support@example.com"
};

// ✅ CORRECT
var options = new SendGridOptions
{
    Key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY")!,
    SendFromEmailAddress = Environment.GetEnvironmentVariable("SENDGRID_FROM_EMAIL")!,
    ReplyToEmailAddress = Environment.GetEnvironmentVariable("SENDGRID_REPLY_TO_EMAIL")!
};
```

### 4. Error Handling

Handle SendGrid API failures gracefully:

```csharp
try
{
    await _emailSender.SendEmailAsync(email, subject, message);
}
catch (SendGridClientException ex) when (ex.ErrorMessages?.Any(e => e.Contains("Unauthorized")) == true)
{
    // Invalid API key
    _logger.LogError("SendGrid API key is invalid");
}
catch (SendGridClientException ex)
{
    // Other SendGrid errors
    _logger.LogError($"Failed to send email: {ex.Message}");
}
catch (HttpRequestException ex)
{
    // Network issues
    _logger.LogError($"Network error sending email: {ex.Message}");
}
```

### 5. Email Validation

Validate recipient addresses before sending:

```csharp
public bool IsValidEmail(string email)
{
    try
    {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
    }
    catch
    {
        return false;
    }
}

public async Task SendEmailIfValid(string email, string subject, string message)
{
    if (!IsValidEmail(email))
    {
        _logger.LogWarning($"Invalid email address: {email}");
        return;
    }

    await _emailSender.SendEmailAsync(email, subject, message);
}
```

### 6. Batch Email Operations

Consider SendGrid's batch capabilities for high-volume sending:

```csharp
public async Task SendBulkNotification(List<string> recipients, string subject, string message)
{
    var tasks = recipients
        .Select(email => _emailSender.SendEmailAsync(email, subject, message))
        .ToList();

    // Send in parallel with rate limiting to avoid API throttling
    var chunks = tasks.Chunk(100); // SendGrid allows ~100 req/sec
    foreach (var chunk in chunks)
    {
        await Task.WhenAll(chunk);
        await Task.Delay(1000); // 1 second delay between batches
    }
}
```

### 7. Logging and Monitoring

Log email operations for debugging and compliance:

```csharp
public class LoggingEmailSender : IEmailSender
{
    private readonly IEmailSender _innerSender;
    private readonly ILogger<LoggingEmailSender> _logger;

    public LoggingEmailSender(IEmailSender innerSender, ILogger<LoggingEmailSender> logger)
    {
        _innerSender = innerSender;
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        _logger.LogInformation($"Sending email to {email} with subject '{subject}'");
        try
        {
            await _innerSender.SendEmailAsync(email, subject, message);
            _logger.LogInformation($"Successfully sent email to {email}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send email to {email}");
            throw;
        }
    }

    public async Task SendEmailAsync(string email, string subject, string message, string from)
    {
        _logger.LogInformation($"Sending email to {email} from {from} with subject '{subject}'");
        try
        {
            await _innerSender.SendEmailAsync(email, subject, message, from);
            _logger.LogInformation($"Successfully sent email to {email}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to send email to {email}");
            throw;
        }
    }
}
```

## Architecture

### Email Flow

```
Application Layer
    ↓
IEmailSender Interface (Abstraction)
    ↓
SendGridEmailClient (Implementation)
    ↓
SendGrid NuGet Package
    ↓
SendGrid REST API
    ↓
Recipient Email System
```

### Configuration Validation

The `SendGridEmailClient` constructor validates all required options:

```
Constructor Called
    ↓
Guard.Against.NullOrWhiteSpace(Key)
Guard.Against.NullOrWhiteSpace(SendFromEmailAddress)
Guard.Against.NullOrWhiteSpace(ReplyToEmailAddress)
    ↓
All Valid → Store in Private Fields
    ↓
ArgumentException → Thrown if Invalid
```

### Email Composition

For each email sent, the client:

1. Creates a new `SendGridClient` with the API key
2. Instantiates a `SendGridMessage`
3. Sets the sender email address
4. Adds recipient address
5. Sets custom reply-to (or uses fallback)
6. Sets subject line
7. Adds HTML content
8. Sends via SendGrid API

## Common Scenarios

### Sending Welcome Email

```csharp
public async Task SendWelcomeEmails(List<User> newUsers)
{
    foreach (var user in newUsers)
    {
        var subject = "Welcome to Our Service!";
        var message = $@"
            <h1>Welcome, {user.FirstName}!</h1>
            <p>Your account has been created successfully.</p>
            <p>
                <a href='https://example.com/dashboard'>
                    Go to Dashboard
                </a>
            </p>
            <p>Questions? <a href='mailto:support@example.com'>Contact us</a></p>
        ";

        await _emailSender.SendEmailAsync(user.Email, subject, message);
    }
}
```

### Sending Password Reset Email

```csharp
public async Task SendPasswordResetEmail(User user, string resetToken)
{
    var resetUrl = $"https://example.com/reset-password?token={resetToken}";
    var expiryTime = DateTime.UtcNow.AddHours(24);

    var subject = "Password Reset Request";
    var message = $@"
        <p>You requested a password reset for {user.Email}</p>
        <p>
            <a href='{resetUrl}' 
               style='background-color: #0069d9; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>
                Reset Password
            </a>
        </p>
        <p>
            This link expires at <strong>{expiryTime:g} UTC</strong>.
        </p>
        <p>
            If you didn't request this, please ignore this email.
        </p>
    ";

    await _emailSender.SendEmailAsync(user.Email, subject, message);
}
```

### Sending Order Confirmation

```csharp
public async Task SendOrderConfirmation(Order order, Customer customer)
{
    var itemsHtml = string.Join("", order.Items.Select(item => $@"
        <tr>
            <td>{item.ProductName}</td>
            <td>{item.Quantity}</td>
            <td>${item.UnitPrice}</td>
            <td>${item.Total}</td>
        </tr>
    "));

    var subject = $"Order Confirmation #{order.OrderNumber}";
    var message = $@"
        <h2>Thank You for Your Order!</h2>
        <p>Order Number: <strong>#{order.OrderNumber}</strong></p>
        <p>Order Date: {order.OrderDate:yyyy-MM-dd}</p>
        
        <table style='width: 100%; border-collapse: collapse; margin: 20px 0;'>
            <thead style='background-color: #f2f2f2;'>
                <tr>
                    <th style='padding: 10px; text-align: left; border: 1px solid #ddd;'>Product</th>
                    <th style='padding: 10px; text-align: center; border: 1px solid #ddd;'>Qty</th>
                    <th style='padding: 10px; text-align: right; border: 1px solid #ddd;'>Price</th>
                    <th style='padding: 10px; text-align: right; border: 1px solid #ddd;'>Total</th>
                </tr>
            </thead>
            <tbody>
                {itemsHtml}
            </tbody>
        </table>
        
        <p style='font-size: 16px; font-weight: bold;'>
            Total Amount: ${order.Total}
        </p>
        
        <p>Your order will be shipped to:</p>
        <p>
            {customer.FullName}<br/>
            {order.ShippingAddress}<br/>
            {order.ShippingCity}, {order.ShippingState} {order.ShippingZip}
        </p>

        <p>Thank you for your business!</p>
    ";

    await _emailSender.SendEmailAsync(customer.Email, subject, message);
}
```

## Troubleshooting

### Invalid API Key

**Error:**
```
SendGridClientException: Unauthorized
```

**Solution:**
1. Verify API key starts with `SG.`
2. Check the key is correct in SendGrid dashboard
3. Ensure key hasn't been revoked
4. Verify environment variable is set correctly

```bash
# Verify environment variable
echo $SENDGRID_API_KEY
```

### Sender Address Not Verified

**Error:**
```
SendGridClientException: Invalid email from address
```

**Solution:**
1. Log into SendGrid dashboard
2. Go to Settings → Sender Authentication
3. Verify the sender address
4. Use verified address in configuration

### Rate Limiting

**Error:**
```
SendGridClientException: Too many requests (429)
```

**Solution:**
Implement exponential backoff:

```csharp
public async Task SendEmailWithRetry(string email, string subject, string message, int maxRetries = 3)
{
    int retryCount = 0;
    TimeSpan delay = TimeSpan.FromSeconds(1);

    while (retryCount < maxRetries)
    {
        try
        {
            await _emailSender.SendEmailAsync(email, subject, message);
            return;
        }
        catch (SendGridClientException ex) when (ex.HttpStatusCode == System.Net.HttpStatusCode.TooManyRequests)
        {
            retryCount++;
            if (retryCount >= maxRetries) throw;
            
            await Task.Delay(delay);
            delay = TimeSpan.FromSeconds(delay.TotalSeconds * 2); // Exponential backoff
        }
    }
}
```

### Network Timeout

**Error:**
```
HttpRequestException: The operation timed out
```

**Solution:**
Implement connection pooling and timeout configuration:

```csharp
var httpClient = new HttpClient
{
    Timeout = TimeSpan.FromSeconds(30)
};

var sendGridClient = new SendGridClient(httpClient, apiKey);
```

## Related Documentation

- [SendGrid Documentation](https://docs.sendgrid.com/)
- [SendGrid Authentication](https://docs.sendgrid.com/for-developers/authentication)
- [SendGrid C# Library](https://github.com/sendgrid/sendgrid-csharp)
- [EXCSLA.Shared Framework Documentation](../../../README.md)
- [IEmailSender Interface](../../Domain/Abstractions/IEmailSender.cs)

## License

Part of the EXCSLA.Shared Domain-Driven Design Framework.
