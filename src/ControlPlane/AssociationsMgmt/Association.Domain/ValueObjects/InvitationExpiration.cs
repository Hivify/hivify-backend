namespace Association.Domain.ValueObjects;

public sealed record InvitationExpiration
{
    private const int ValidityDays = 5;

    public DateTime CreatedAt { get; }
    public DateTime ExpiresAt { get; }

    private InvitationExpiration(DateTime createdAt)
    {
        CreatedAt = createdAt;
        ExpiresAt = createdAt.AddDays(ValidityDays);
    }

    public static InvitationExpiration Create(DateTime createdAt)
    {
        return new InvitationExpiration(createdAt);
    }

    public bool IsExpired(DateTime utcNow)
    {
        return utcNow >= ExpiresAt;
    }

    public bool IsValid(DateTime utcNow)
    {
        return utcNow < ExpiresAt;
    }
}