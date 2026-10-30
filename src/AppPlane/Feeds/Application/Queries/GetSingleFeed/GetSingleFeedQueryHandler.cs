using BuildingBlocks.ApplicationPorts.AssociationPorts;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Feeds.Application.Contracts;
using Feeds.Application.DTOs;
using Feeds.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Feeds.Application.Queries.GetSingleFeed;

public sealed class GetSingleFeedQueryHandler
    : IQueryHandler<GetSingleFeedQuery, FeedListItem?>
{
    private readonly IFeedRepo _feedRepository;
    private readonly ICurrentAssociationAsTenant _currentTenant;

    public GetSingleFeedQueryHandler(
        IFeedRepo feedRepository,
        ICurrentAssociationAsTenant currentTenant)
    {
        _feedRepository = feedRepository;
        _currentTenant = currentTenant;
    }

    public async Task<FeedListItem?> Handle(
        GetSingleFeedQuery query,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var associationId = _currentTenant.AssociationId;

        if (associationId is null || associationId == Guid.Empty)
        {
            return null;
        }

        var feed = await _feedRepository.GetByIdAsync(
            new FeedID(query.FeedId),
            new AssociationID(associationId.Value),
            cancellationToken);

        if (feed is null)
        {
            return null;
        }

        return new FeedListItem(
            feed.Id.Value,
            feed.Title.Value,
            feed.Content.Value,
            feed.CreatedDate);
    }
}