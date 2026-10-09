using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Houses.Application.Contracts;
using Houses.Application.DTOs;

namespace Houses.Application.Queries.GetHouses;

public sealed class GetHousesQueryHandler
    : IQueryHandler<
        GetHousesQuery,
        IReadOnlyList<HouseListItem>>
{
    private readonly IHouseRepo _houseRepo;

    public GetHousesQueryHandler(
        IHouseRepo houseRepo)
    {
        _houseRepo = houseRepo;
    }

    public async Task<IReadOnlyList<HouseListItem>> Handle(
        GetHousesQuery query,
        CancellationToken cancellationToken)
    {
        var houses =
            await _houseRepo.GetAllAsync(cancellationToken);

        return houses
            .Select(house =>
                new HouseListItem(
                    house.Id.Value,
                    house.Address.Value,
                    house.HouseNumber.Value,
                    house.PostalCode.Value,
                    house.CreatedAt))
            .ToList();
    }
}