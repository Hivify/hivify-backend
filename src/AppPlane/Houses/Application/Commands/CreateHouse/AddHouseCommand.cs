using BuildingBlocks.ApplicationPorts.Contracts.Messaging;

namespace Houses.Application.Commands.CreateHouse
{
    public sealed record AddHouseCommand(
        string Address,
        string HouseNumber,
        string PostalCode) : ICommand<Guid>
    {
    }
}
