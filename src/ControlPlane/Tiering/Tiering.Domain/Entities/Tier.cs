using SharedKernel;
using SharedKernel.ValueObjects;
using Tiering.Domain.Enums;
using Tiering.Domain.ValueObjcts;

namespace Tiering.Domain.Entities;

public sealed class Tier : BaseEntity<TierID>, IAggregateRoot
{
    public Name TierName { get; private set; }

    public IntNumber MaxMembers { get; private set; }

    public IntNumber MaxAdministrators { get; private set; }

    public TierStatus Status { get; private set; }

    private Tier() { }

    private Tier(
        TierID id,
        Name tierName,
        IntNumber maxMembers,
        IntNumber maxAdministrators)
        : base(id)
    {
        TierName = tierName;
        MaxMembers = maxMembers;
        MaxAdministrators = maxAdministrators;
        Status = TierStatus.Active;
    }

    public static Tier Create(
        Name tierName,
        IntNumber maxMembers,
        IntNumber maxAdministrators)
    {
        return new Tier(
            new TierID(Guid.NewGuid()),
            tierName,
            maxMembers,
            maxAdministrators);
    }

    public void UpdateLimits(
    IntNumber maxMembers,
    IntNumber maxAdministrators)
    {
        MaxMembers = maxMembers;
        MaxAdministrators = maxAdministrators;
    }

    public void Deactivate()
    {
        Status = TierStatus.Inactive;
    }

    public void Activate()
    {
        Status = TierStatus.Active;
    }
}