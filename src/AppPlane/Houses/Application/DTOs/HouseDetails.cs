namespace Houses.Application.DTOs;

public sealed record HouseDetails(
    Guid Id,
    string Address,
    string HouseNumber,
    string PostalCode,
    DateTime CreatedAt,
    IReadOnlyCollection<HouseListItem> Tenants
);