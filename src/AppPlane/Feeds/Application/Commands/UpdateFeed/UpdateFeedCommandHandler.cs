using BuildingBlocks.ApplicationPorts.AssociationUserPort;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.HivifyUserPort;
using Feeds.Application.Contracts;
using Feeds.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Feeds.Application.Commands.UpdateFeed;

public sealed class UpdateFeedCommandHandler
    : ICommandHandler<UpdateFeedCommand, bool>
{
    private readonly IFeedRepo _feedRepository;
    private readonly ICurrentAssociationMember _currentTenant;
    private readonly ICurrentHivifyUser _currentUser;

    public UpdateFeedCommandHandler(
        IFeedRepo feedRepository,
        ICurrentAssociationMember currentTenant,
        ICurrentHivifyUser currentUser)
    {
        _feedRepository = feedRepository;
        _currentTenant = currentTenant;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        UpdateFeedCommand command,
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

        feed.Update(
            new Title(command.Title),
            new Description(command.Content));

        await _feedRepository.UpdateFeedAsync(
            feed,
            cancellationToken);

        return true;
    }
}