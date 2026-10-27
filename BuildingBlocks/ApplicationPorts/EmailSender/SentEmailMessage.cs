namespace BuildingBlocks.ApplicationPorts.EmailSender;

public sealed record SentEmailMessage(
    string To,
    string Subject,
    string HtmlBody,
    string? TextBody = null);