using BuildingBlocks.ApplicationPorts.Messaging;
using Feeds.Application.DTOs;

namespace Feeds.Application.Queries.GetSingleFeed;

public sealed record GetSingleFeedQuery(Guid FeedId) : IQuery<FeedListItem>;
