using SharedKernel;

namespace Feeds.Domain.Feeds
{
    public readonly record struct FeedID(Guid Value) : IValue
    {
    }
}
