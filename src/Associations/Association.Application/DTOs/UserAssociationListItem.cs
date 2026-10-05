namespace Association.Application.DTOs
{
    public sealed record UserAssociationListItem(
      Guid AssociationId,
      string Name,
      string Identifier,
      string Role);
}
