using Association.Application.Commands.AcceptMemberInvitation;
using Association.Application.Commands.CreateAssociation;
using Association.Application.Commands.InviteMember;
using Association.Application.DTOs;
using Association.Application.Queries.GetAllAssociationByUser;
using Association.Application.Queries.GetAllAssociations;
using Association.Application.Queries.GetAssociationByIdentifier;
using Association.Application.Queries.GetAssociationDetailsById;
using Association.Application.Queries.GetAssociationMembers;
using Association.Application.Queries.GetInvitationDetailsQuery;
using Association.Application.Queries.GetUserAssociationClaims;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Association.Application;

public static class ApplicationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAssociationServices()
        {
            services.AddScoped<
                ICommandHandler<CreateAssociationCommand, Guid>,
                CreateAssociationCommandHandler>();

            services.AddScoped<
           ICommandHandler<InviteMemberCommand, Guid>,
           InviteMemberCommandHandler>();

            services.AddScoped<
                IQueryHandler<GetAllAssociationsQuery, IReadOnlyList<AssociationDto>>,
                GetAllAssociationsQueryHandler>();

            services.AddScoped<
                IQueryHandler<GetAssociationByIdentifierQuery, AssociationDto?>,
                GetAssociationByIdentifierQueryHandler>();

            services.AddScoped<
                IQueryHandler<GetUserAssociationClaimsQuery, IReadOnlyCollection<Claim>>,
                GetUserAssociationClaimsQueryHandler>();


            services.AddScoped<
                IQueryHandler<GetCurrentAssociationDetailsQuery, AssociationDto?>,
                GetCurrentAssociationDetailsQueryHandler>();

            services.AddScoped<IQueryHandler<GetInvitationDetailsQuery, InvitationDetailsDto?>, GetInvitationDetailsHandler>();

            services.AddScoped<ICommandHandler<AcceptInvitationCommand, Guid>, AcceptInvitationCommandHandler>();

            services.AddScoped<IQueryHandler<GetAssociationMembersQuery, IReadOnlyList<AssociationMemberDto>>, GetAssociationMembersQueryHandler>();

            services.AddScoped<IQueryHandler<GetAllAssociationsByUserQuery, IReadOnlyList<UserAssociationDto>>, GetAllAssociationsByUserQueryHandler>();


            return services;
        }
    }
}
