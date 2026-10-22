using Feeds.Domain.Feeds;
using Feeds.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Feeds.Application.Contracts;

public interface IFeedRepo
{
    Task CreateFeedAsync(
        Feed feed,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Feed>> GetAllFeedsAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Feed>> GetAllByDateAsync(
        DateTime createdDate,
        CancellationToken cancellationToken = default);

    Task<Feed?> GetByIdAsync(
    FeedID id,
    AssociationID associationId,
    CancellationToken cancellationToken = default);
    Task UpdateFeedAsync(
        Feed feed,
        CancellationToken cancellationToken = default);
}