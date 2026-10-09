using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Houses.Application.DTOs;

namespace Houses.Application.Queries.GetHouseTenants;

public sealed record GetHouseTenantsQuery(Guid HouseId) : IQuery<IReadOnlyList<TenantListItem>>;