using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Queries.GetInvitationDetailsQuery;



public sealed record GetInvitationDetailsQuery(string Token) : IQuery<InvitationDetailsDto>;
