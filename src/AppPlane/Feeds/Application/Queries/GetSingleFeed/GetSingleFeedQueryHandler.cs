using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Feeds.Application.Contracts;
using Feeds.Application.DTOs;
using Feeds.Application.Queries.GetSingleFeed;
using Feeds.Domain.Feeds;


public sealed class GetSingleFeedQueryHandler : IQueryHandler<GetSingleFeedQuery, FeedListItem>
{
    private readonly IFeedRepo _feedRepository;
    public GetSingleFeedQueryHandler(IFeedRepo feedRepository)
    {
        _feedRepository = feedRepository;
    }
    public async Task<FeedListItem> Handle(GetSingleFeedQuery query, CancellationToken cancellationToken)
    {
        var feed = await _feedRepository.GetByIdAsync(new FeedID(query.FeedId), cancellationToken);

        if (feed is null)
            throw new InvalidOperationException("Feed was not found.");
        return new FeedListItem(
            feed.Id.Value,
            feed.Title.Value,
            feed.Content.Value,
            feed.CreatedDate);
    }
}
