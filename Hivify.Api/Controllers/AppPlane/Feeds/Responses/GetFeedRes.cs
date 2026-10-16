namespace Hivify.Api.Controllers.AppPlane.Feeds.Responses;


public sealed record GetFeedRes(
    Guid Id,
    string Title,
    string Content,
    DateTime CreatedDate);