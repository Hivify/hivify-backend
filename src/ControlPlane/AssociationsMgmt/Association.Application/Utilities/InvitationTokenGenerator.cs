using System.Security.Cryptography;

namespace Association.Application.Utilities;

public static class InvitationTokenGenerator
{
    public static string Generate()
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(tokenBytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}