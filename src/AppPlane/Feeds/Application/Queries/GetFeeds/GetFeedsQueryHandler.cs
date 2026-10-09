using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Feeds.Application.Contracts;
using Feeds.Application.DTOs;

namespace Feeds.Application.Queries.GetFeeds;

public sealed class GetFeedsQueryHandler : IQueryHandler<GetFeedsQuery, IReadOnlyList<FeedListItem>>
{
    private readonly IFeedRepo _feedRepository;

    public GetFeedsQueryHandler(
        IFeedRepo feedRepository)
    {
        _feedRepository = feedRepository;
    }

    public async Task<IReadOnlyList<FeedListItem>> Handle(
        GetFeedsQuery query,
        CancellationToken cancellationToken)
    {
        var feeds =
            await _feedRepository.GetAllFeedsAsync(
                cancellationToken);

        return feeds
            .OrderByDescending(f => f.CreatedDate)
            .Select(f => new FeedListItem(
                f.Id.Value,
                f.Title.Value,
                f.Content.Value,
                f.CreatedDate))
            .ToList();
    }
}