using BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Feeds.Application.Contracts;
using Feeds.Domain.ValueObjects;

namespace Feeds.Application.Commands.DeleteFeed;

public sealed class DeleteFeedCommandHandler
    : ICommandHandler<DeleteFeedCommand, bool>
{
    private readonly IFeedRepo _feedRepository;
    private readonly ICurrentUser _currentUser;

    public DeleteFeedCommandHandler(
        IFeedRepo feedRepository,
        ICurrentUser currentUser)
    {
        _feedRepository = feedRepository;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        DeleteFeedCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (!_currentUser.IsInRole("Admin"))
        {
            throw new UnauthorizedAccessException(
                "Only administrators can delete feeds.");
        }

        var feedId = new FeedID(command.FeedId);

        var feed = await _feedRepository.GetByIdAsync(
            feedId,
            cancellationToken);

        if (feed is null)
        {
            throw new KeyNotFoundException(
                $"Feed {command.FeedId} was not found.");
        }

        feed.Delete();

        await _feedRepository.UpdateFeedAsync(
            feed,
            cancellationToken);

        return true;
    }
}