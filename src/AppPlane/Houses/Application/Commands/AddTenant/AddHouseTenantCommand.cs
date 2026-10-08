using BuildingBlocks.ApplicationPorts.Contracts.Messaging;

namespace Houses.Application.Commands.AddTenant;

public sealed record AddHouseTenantCommand(Guid HouseId, Guid UserId, string Email, string FullName, string PhoneNumber) : ICommand<Guid>;