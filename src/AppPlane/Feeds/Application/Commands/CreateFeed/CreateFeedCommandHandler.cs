using BuildingBlocks.ApplicationPorts.AssociationPorts;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.HivifyUserPort;
using Feeds.Application.Contracts;
using Feeds.Domain.Feeds;
using SharedKernel.ValueObjects;

namespace Feeds.Application.Commands.CreateFeed;

public sealed class CreateFeedCommandHandler
    : ICommandHandler<CreateFeedCommand, Guid>
{
    private readonly IFeedRepo _feedRepository;
    private readonly ICurrentHivifyUser _currentUser;
    private readonly ICurrentAssociationTenant _currentTenant;

    public CreateFeedCommandHandler(
        IFeedRepo feedRepository,
        ICurrentHivifyUser currentUser,
        ICurrentAssociationTenant currentTenant)
    {
        _feedRepository = feedRepository;
        _currentUser = currentUser;
        _currentTenant = currentTenant;
    }

    public async Task<Guid> Handle(
        CreateFeedCommand command,
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

        var userId = _currentUser.UserId;

        if (userId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "A valid user ID is required.");
        }

        var feed = Feed.CreateFeed(
            new AssociationID(associationId.Value),
            new UserID(userId),
            new Title(command.Title),
            new Description(command.Content));

        await _feedRepository.CreateFeedAsync(
            feed,
            cancellationToken);

        return feed.Id.Value;
    }
}