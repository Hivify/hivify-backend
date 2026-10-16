using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Messaging;
using Feeds.Application.Contracts;
using Feeds.Domain.Feeds;
using SharedKernel.ValueObjects;
namespace Feeds.Application.Commands.CreateFeed;

public sealed class CreateFeedCommandHandler
    : ICommandHandler<CreateFeedCommand, Guid>
{
    private readonly IFeedRepo _feedRepository;
    private readonly ICurrentUser _currentUser;

    public CreateFeedCommandHandler(
        IFeedRepo feedRepository,
        ICurrentUser currentUser)
    {
        _feedRepository = feedRepository;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(
        CreateFeedCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (!_currentUser.IsInRole("Admin") || _currentUser.UserId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "Only administrators can create feeds.");
        }

        var userId = _currentUser.UserId;

        var feed = Feed.CreateFeed(
            new UserID(userId),
            new Title(command.Title),
            new Description(command.Content));

        await _feedRepository.CreateFeedAsync(
            feed,
            cancellationToken);

        return feed.Id.Value;
    }
}