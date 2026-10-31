using System.Security.Cryptography;
using System.Text;

namespace Association.Domain.ValueObjects;

public sealed record InvitationTokenHash
{
    public string Value { get; }

    private InvitationTokenHash(string value)
    {
        Value = value;
    }

    // Used when receiving the original token.
    public static InvitationTokenHash Create(string originalToken)
    {
        if (string.IsNullOrWhiteSpace(originalToken))
            throw new ArgumentException("Token cannot be empty.", nameof(originalToken));

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(originalToken));

        return new InvitationTokenHash(Convert.ToHexString(hash));
    }

    // Used by EF Core when reading the stored hash.
    public static InvitationTokenHash FromStoredValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            value.Length != 64 ||
            !value.All(Uri.IsHexDigit))
        {
            throw new ArgumentException("Invalid invitation token hash.", nameof(value));
        }

        return new InvitationTokenHash(value.ToUpperInvariant());
    }
}