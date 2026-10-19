using Association.Domain.Enums;

namespace Association.Application.DTOs
{
    public sealed record TenantAssociationOutput(
      Guid AssociationId,
      string Name,
      Guid Identifier,
      string Status,
      DateTime CreatedDate);



    public sealed record AssociationDetailsOutput(
    Guid AssociationId,
    string Name,
    Guid Identifier,
    string Status,
    DateTime CreatedDate,
    Guid OwnerUserId,
    string OwnerEmail,
    int MemberCount);


    public sealed record AssociationDetailsData(
    Guid AssociationId,
    string Name,
    Guid Identifier,
    AssociationStatus Status,
    DateTime CreatedDate,
    Guid OwnerUserId,
    string OwnerEmail,
    int MemberCount);



    public sealed record UserAssociationOutput(
        Guid AssociationId,
        string AssociationName,
        string Status,
        string Role,
        DateTime JoinedAt);
}


