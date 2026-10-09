using SharedKernel;

namespace Feeds.Domain.ValueObjects
{
    public readonly record struct FeedID(Guid Value) : IValue
    {
    }
}
