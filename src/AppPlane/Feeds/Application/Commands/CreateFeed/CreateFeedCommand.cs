using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Feeds.Application.Commands.CreateFeed;


public sealed record CreateFeedCommand(string Title, string Content) : ICommand<Guid>;