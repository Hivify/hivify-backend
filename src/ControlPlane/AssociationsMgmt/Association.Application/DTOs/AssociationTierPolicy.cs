namespace Association.Application.DTOs;

public sealed record AssociationTierPolicy(
    int MaxMembers,
    int MaxAdministrators,
    IReadOnlyCollection<string> Features);