using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using SharedKernel;
using SharedKernel.ValueObjects;

namespace Association.Domain.Entities;

public class AssociationInvitation : BaseEntity<InvitationID>, IAggregateRoot
{
    public AssociationID AssociationID { get; private set; }
    public UserID InvitedByUserID { get; private set; }
    public ContactEmail Email { get; private set; }
    public InvitationTokenHash TokenHash { get; private set; }
    public InvitationStatus Status { get; private set; }

    public InvitationExpiration Expiration { get; private set; }

    public DateTimeOffset? AcceptedAt { get; private set; }

    private AssociationInvitation()
    {
    }

    private AssociationInvitation(
        InvitationID id,
        AssociationID associationID,
        UserID invitedByUserID,
        ContactEmail email,
        InvitationTokenHash tokenHash,
        InvitationExpiration expiration)
        : base(id)
    {
        AssociationID = associationID;
        InvitedByUserID = invitedByUserID;
        Email = email;
        TokenHash = tokenHash;
        Expiration = expiration;
        Status = InvitationStatus.Pending;
    }

    public static AssociationInvitation Create(
        AssociationID associationID,
        UserID invitedByUserID,
        ContactEmail email,
        InvitationTokenHash tokenHash)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(tokenHash);

        var expiration = InvitationExpiration.Create(DateTimeOffset.UtcNow);

        return new AssociationInvitation(
            new InvitationID(Guid.NewGuid()),
            associationID,
            invitedByUserID,
            email,
            tokenHash,
            expiration);
    }

    public void Accept()
    {
        EnsurePending();

        if (Expiration.IsExpired(DateTimeOffset.UtcNow))
        {
            throw new InvalidOperationException(
                "This invitation has expired.");
        }

        Status = InvitationStatus.Accepted;
        AcceptedAt = DateTimeOffset.UtcNow;
    }

    public void Revoke()
    {
        EnsurePending();

        Status = InvitationStatus.Revoked;
    }

    public void MarkExpired()
    {
        EnsurePending();

        if (!Expiration.IsExpired(DateTimeOffset.UtcNow))
        {
            throw new InvalidOperationException(
                "This invitation has not expired.");
        }

        Status = InvitationStatus.Expired;
    }

    public bool IsValid(DateTimeOffset utcNow)
    {
        return Status == InvitationStatus.Pending
            && !Expiration.IsExpired(utcNow);
    }

    private void EnsurePending()
    {
        if (Status != InvitationStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending invitations can be changed.");
        }
    }
}