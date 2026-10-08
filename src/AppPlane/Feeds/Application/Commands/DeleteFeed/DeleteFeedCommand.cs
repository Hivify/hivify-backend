using BuildingBlocks.ApplicationPorts.Contracts.Messaging;

namespace Feeds.Application.Commands.DeleteFeed;

public sealed record DeleteFeedCommand(Guid FeedId) : ICommand<bool>;