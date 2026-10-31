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

    public static InvitationTokenHash Create(string originalToken)
    {
        if (string.IsNullOrWhiteSpace(originalToken))
            throw new ArgumentException("Token cannot be empty.", nameof(originalToken));

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(originalToken));

        return new InvitationTokenHash(Convert.ToHexString(hash));
    }
}