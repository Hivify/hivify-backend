namespace Tiering.Application.DTOs;

public sealed record TierPolicyDto(
    int MaxMembers,
    int MaxAdministrators,
    IReadOnlyCollection<string> Features);