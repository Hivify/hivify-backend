namespace BuildingBlocks.ApplicationPorts.Email;

public sealed record ConfirmationMessage(
    string To,
    string Subject,
    string HtmlBody,
    string? TextBody = null);