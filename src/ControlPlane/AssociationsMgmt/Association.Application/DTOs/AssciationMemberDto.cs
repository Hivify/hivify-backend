namespace Association.Application.DTOs;



public sealed record AssciationMemberDto(
Guid AssociationId,
string AssociationName,
string Status,
string Role,
DateTime JoinedAt);
