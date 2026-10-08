namespace Association.Application.DTOs
{
    public sealed record TenantAssociationOutput(
      Guid AssociationId,
      string Name,
      Guid Identifier,
      string Role);
}
