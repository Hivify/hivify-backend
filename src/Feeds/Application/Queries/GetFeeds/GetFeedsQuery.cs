using BuildingBlocks.ApplicationPorts.Messaging;
using Feeds.Application.DTOs;

namespace Feeds.Application.Queries.GetFeeds;

public sealed record GetFeedsQuery : IQuery<IReadOnlyList<FeedListItem>>;