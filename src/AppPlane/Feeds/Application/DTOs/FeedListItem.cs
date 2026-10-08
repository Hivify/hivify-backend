namespace Feeds.Application.DTOs;

public sealed record FeedListItem(
    Guid Id,
    string Title,
    string Content,
    DateTime CreatedDate);