using BuildingBlocks.ApplicationPorts.Messaging;
using Houses.Application.Contracts;
using Houses.Domain.Houses;

namespace Houses.Application.Commands.UpdateHouse;

public sealed class UpdateHouseCommandHandler : ICommandHandler<UpdateHouseCommand, bool>
{
    private readonly IHouseRepo _houseRepo;

    public UpdateHouseCommandHandler(IHouseRepo houseRepo)
    {
        _houseRepo = houseRepo;
    }

    public async Task<bool> Handle(UpdateHouseCommand command, CancellationToken cancellationToken)
    {
        var house = await _houseRepo.GetByIdAsync(
            new HouseID(command.HouseId), cancellationToken);

        if (house is null)
            return false;

        house.Update(
            new Address(command.Address),
            new HouseNumber(command.HouseNumber),
            new PostalCode(command.PostalCode));

        await _houseRepo.SaveChangesAsync(cancellationToken);

        return true;
    }
}