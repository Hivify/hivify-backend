using BuildingBlocks.ApplicationPorts.Contracts.Messaging;

namespace Feeds.Application.Commands.CreateFeed;


public sealed record CreateFeedCommand(string Title, string Content) : ICommand<Guid>;