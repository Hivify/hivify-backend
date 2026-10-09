using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Houses.Application.Contracts;
using Houses.Application.DTOs;
using Houses.Domain.Houses;


namespace Houses.Application.Queries.GetHouseTenants;

public sealed class GetHouseTenantsQueryHandler : IQueryHandler<GetHouseTenantsQuery, IReadOnlyList<TenantListItem>>
{
    private readonly IHouseRepo _houseRepo;

    public GetHouseTenantsQueryHandler(IHouseRepo houseRepo)
    {
        _houseRepo = houseRepo;
    }

    public async Task<IReadOnlyList<TenantListItem>> Handle(GetHouseTenantsQuery query, CancellationToken cancellationToken)
    {
        var house = await _houseRepo.GetByIdAsync(
            new HouseID(query.HouseId),
            cancellationToken);

        if (house is null)
            throw new InvalidOperationException(
                "House could not be found.");

        return house.Tenants
            .Where(t => t.DeletedAt == null)
            .Select(t => new TenantListItem(
                t.Id.Value,
                t.UserId.Value,
                t.Email.Value,
                t.FullName.Value,
                t.PhoneNumber.Value,
                t.CreatedAt))
            .ToList();
    }
}