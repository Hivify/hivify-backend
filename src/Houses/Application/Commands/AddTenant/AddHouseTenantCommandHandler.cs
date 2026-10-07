using BuildingBlocks.ApplicationPorts.Messaging;
using Houses.Application.Commands.AddTenant;
using Houses.Application.Contracts;
using Houses.Domain.Houses;
using Identity.Application.Contracts;
using SharedKernel.ValuesObjects;

public sealed class AddHouseTenantCommandHandler : ICommandHandler<AddHouseTenantCommand, Guid>
{
    private readonly IHouseRepo _houseRepo;
    private readonly IUserHivifyDirectory _userManagementService;

    public AddHouseTenantCommandHandler(IHouseRepo houseRepo, IUserHivifyDirectory userManagementService)
    {
        _houseRepo = houseRepo;
        _userManagementService = userManagementService;
    }

    public async Task<Guid> Handle(AddHouseTenantCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManagementService.GetByIdAsync(command.UserId, cancellationToken);

        if (user is null)
            throw new InvalidOperationException(
                "The selected user does not exist.");

        var house = await _houseRepo.GetByIdAsync(new HouseID(command.HouseId), cancellationToken);

        if (house is null)
            throw new InvalidOperationException(
                "House could not be found.");

        var tenant = house.AddTenant(new UserID(command.UserId), new Email(command.Email), new Name(command.FullName), new PhoneNumber(command.PhoneNumber));

        await _houseRepo.SaveChangesAsync(cancellationToken);

        return tenant.Id.Value;
    }
}