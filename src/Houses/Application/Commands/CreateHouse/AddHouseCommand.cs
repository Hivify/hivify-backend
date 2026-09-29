using BuildingBlocks.ApplicationPorts.Messaging;


namespace Houses.Application.Commands.CreateHouse
{
    public sealed record AddHouseCommand(
        string Address,
        string HouseNumber,
        string PostalCode) : ICommand<Guid>
    {
    }
}
