using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Feeds.Application.Commands.UpdateFeed;

public sealed record UpdateFeedCommand(Guid FeedId, string Title, string Content) : ICommand<bool>;