
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
    public string TokenHash { get; private set; }
    public InvitationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime? AcceptedAt { get; private set; }

    private AssociationInvitation()
    {
        TokenHash = string.Empty;
    }

    private AssociationInvitation(
        InvitationID id,
        AssociationID associationID,
        UserID invitedByUserID,
        ContactEmail email,
        string tokenHash,
        DateTime expiresAt)
        : base(id)
    {
        AssociationID = associationID;
        InvitedByUserID = invitedByUserID;
        Email = email;
        TokenHash = tokenHash;
        Status = InvitationStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
    }

    public static AssociationInvitation Create(
        AssociationID associationID,
        UserID invitedByUserID,
        ContactEmail email,
        string tokenHash,
        DateTime expiresAt)
    {
        if (associationID.Value == Guid.Empty)
            throw new ArgumentException("Association ID cannot be empty.");

        if (invitedByUserID.Value == Guid.Empty)
            throw new ArgumentException("Inviter user ID cannot be empty.");

        ArgumentNullException.ThrowIfNull(email);

        if (string.IsNullOrWhiteSpace(tokenHash))
            throw new ArgumentException("Token hash is required.");

        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Expiration must be in the future.");

        return new AssociationInvitation(
            new InvitationID(Guid.NewGuid()),
            associationID,
            invitedByUserID,
            email,
            tokenHash,
            expiresAt);
    }

    public void Accept()
    {
        EnsurePending();

        if (DateTime.UtcNow >= ExpiresAt)
        {
            Status = InvitationStatus.Expired;
            throw new InvalidOperationException(
                "This invitation has expired.");
        }

        Status = InvitationStatus.Accepted;
        AcceptedAt = DateTime.UtcNow;
    }

    public void Revoke()
    {
        EnsurePending();

        Status = InvitationStatus.Revoked;
    }

    public void MarkExpired()
    {
        EnsurePending();

        if (DateTime.UtcNow < ExpiresAt)
        {
            throw new InvalidOperationException(
                "This invitation has not expired.");
        }

        Status = InvitationStatus.Expired;
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
