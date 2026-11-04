namespace Association.Application.DTOs;

public sealed record UserAssociationDto(
    Guid AssociationId,
    string AssociationName,
    string Status,
    string UserRole,
    DateTime JoinedAt
);