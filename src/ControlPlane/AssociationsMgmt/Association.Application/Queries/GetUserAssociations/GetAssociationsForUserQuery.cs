using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetUserAssociations;




public sealed record GetAssociationsForUserQuery(Guid UserId) : IQuery<IReadOnlyList<UserAssociationOutput>>;