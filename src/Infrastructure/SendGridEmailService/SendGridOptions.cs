namespace EXCSLA.Shared.Infrastructure;

public class SendGridOptions
{
    public required string Key { get; set; }
    public required string SendFromEmailAddress { get; set; }
    public required string ReplyToEmailAddress { get; set; }
}
