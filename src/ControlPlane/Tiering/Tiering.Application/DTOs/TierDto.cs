namespace Tiering.Application.DTOs;

public sealed record TierDto(
    Guid TierId,
    string Name,
    int MaxMembers,
    int MaxAdministrators,
    string Status);