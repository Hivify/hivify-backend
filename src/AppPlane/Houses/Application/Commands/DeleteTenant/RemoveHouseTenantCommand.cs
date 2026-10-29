using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Houses.Application.Commands.DeleteTenant;

public sealed record RemoveHouseTenantCommand(
    Guid HouseId,
    Guid TenantId)
    : ICommand<bool>;