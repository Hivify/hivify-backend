using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.CurrentUserPort;
using Feeds.Application.Contracts;
using Feeds.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Feeds.Application.Commands.DeleteFeed;

public sealed class DeleteFeedCommandHandler
    : ICommandHandler<DeleteFeedCommand, bool>
{
    private readonly IFeedRepo _feedRepository;
    private readonly ICurrentTenant _currentTenant;
    private readonly ICurrentUser _currentUser;

    public DeleteFeedCommandHandler(
        IFeedRepo feedRepository,
        ICurrentTenant currentTenant,
        ICurrentUser currentUser)
    {
        _feedRepository = feedRepository;
        _currentTenant = currentTenant;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        DeleteFeedCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (!_currentUser.IsAuthenticated)
        {
            throw new UnauthorizedAccessException(
                "Authentication is required.");
        }

        var associationId = _currentTenant.AssociationId;

        if (associationId is null || associationId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "A valid association is required.");
        }

        var feed = await _feedRepository.GetByIdAsync(
            new FeedID(command.FeedId),
            new AssociationID(associationId.Value),
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