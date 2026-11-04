namespace Association.Application.DTOs;

public sealed record AssociationMemberDto(
    Guid UserId,
    string Role,
    DateTime JoinedAt
);