namespace Association.Application.DTOs
{

    public sealed record AssociationDto(
       Guid AssociationId,
       string Name,
       string ContactEmail,
       Guid Identifier,
       string Status,
       Guid OwnerUserId,
       int MemberCount,
       DateTime CreatedDate
   );
}


