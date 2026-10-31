public sealed record InvitationExpiration
{
    private const int ValidityDays = 5;

    public DateTimeOffset CreatedAt { get; }
    public DateTimeOffset ExpiresAt { get; }

    private InvitationExpiration(DateTimeOffset createdAt)
    {
        CreatedAt = createdAt;
        ExpiresAt = createdAt.AddDays(ValidityDays);
    }

    public static InvitationExpiration Create(DateTimeOffset createdAt)
    {
        return new InvitationExpiration(createdAt);
    }

    public bool IsExpired(DateTimeOffset utcNow)
        => utcNow >= ExpiresAt;

    public bool IsValid(DateTimeOffset utcNow)
        => utcNow < ExpiresAt;
}