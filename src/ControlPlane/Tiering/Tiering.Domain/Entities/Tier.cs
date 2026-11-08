using SharedKernel;
using Tiering.Domain.ValueObjcts;

namespace Tiering.Domain.Entities
{
    public sealed class Tier : BaseEntity<TierID>, IAggregateRoot
    {
        public TierName Name { get; private set; }

        public int MaxMembers { get; private set; }

        public int MaxAdministrators { get; private set; }

        public TierStatus Status { get; private set; }
    }
}
