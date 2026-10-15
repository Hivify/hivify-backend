using BuildingBlocks.ApplicationPorts.Messaging;
using Houses.Application.Contracts;
using Houses.Application.DTOs;
using Houses.Domain.Houses;

namespace Houses.Application.Queries.GetHouse;

public sealed class GetHouseQueryHandler
    : IQueryHandler<GetHouseQuery, HouseListItem>
{
    private readonly IHouseRepo _houseRepo;

    public GetHouseQueryHandler(
        IHouseRepo houseRepo)
    {
        _houseRepo = houseRepo;
    }

    public async Task<HouseListItem> Handle(
        GetHouseQuery query,
        CancellationToken cancellationToken)
    {
        var house = await _houseRepo.GetByIdAsync(
            new HouseID(query.HouseId),
            cancellationToken);

        if (house is null)
        {
            throw new InvalidOperationException(
                "House could not be found.");
        }

        return new HouseListItem(
            house.Id.Value,
            house.Address.Value,
            house.HouseNumber.Value,
            house.PostalCode.Value,
            house.CreatedAt);
    }
}