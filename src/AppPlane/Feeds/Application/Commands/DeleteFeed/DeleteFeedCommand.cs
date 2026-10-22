using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Feeds.Application.Commands.DeleteFeed;

public sealed record DeleteFeedCommand(Guid FeedId) : ICommand<bool>;