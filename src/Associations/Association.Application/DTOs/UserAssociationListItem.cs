namespace Association.Application.DTOs
{
    public sealed record UserAssociationListItem(
      Guid AssociationId,
      string Name,
      Guid Identifier,
      string Role);
}
