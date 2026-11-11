namespace Association.Application.DTOs;


public sealed record AssociationDto(
   Guid AssociationId,
   Guid TierId,
   string Name,
   string ContactEmail,
   Guid Identifier,
   string Status,
   Guid OwnerUserId,
   int MemberCount,
   DateTime CreatedDate
);


