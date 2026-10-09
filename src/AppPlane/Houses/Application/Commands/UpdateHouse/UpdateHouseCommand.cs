using BuildingBlocks.ApplicationPorts.Contracts.Messaging;

namespace Houses.Application.Commands.UpdateHouse;

public sealed record UpdateHouseCommand(
    Guid HouseId,
    string Address,
    string HouseNumber,
    string PostalCode) : ICommand<bool>;