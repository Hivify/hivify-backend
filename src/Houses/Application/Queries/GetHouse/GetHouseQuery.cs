using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Houses.Application.DTOs;

namespace Houses.Application.Queries.GetHouse;

public sealed record GetHouseQuery(Guid HouseId) : IQuery<HouseListItem>;