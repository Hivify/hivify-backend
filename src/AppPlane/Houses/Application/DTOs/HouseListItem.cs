namespace Houses.Application.DTOs
{
    public sealed record HouseListItem(
       Guid Id,
       string Address,
       string HouseNumber,
       string PostalCode,
       DateTime CreatedAt
        );
}
