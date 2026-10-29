using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Houses.Application.DTOs;

namespace Houses.Application.Queries.GetHouse;

public sealed record GetHouseQuery(Guid HouseId) : IQuery<HouseListItem>;