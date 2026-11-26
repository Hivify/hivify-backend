
using Association.Application.Commands.AcceptMemberInvitation;
using Association.Application.Commands.CreateAssociation;
using Association.Application.Commands.InviteMember;
using Association.Application.Commands.RemoveMember;
using Association.Application.Context;
using Association.Application.DTOs;
using Association.Application.Queries.GetAllAssociationByUser;
using Association.Application.Queries.GetAllAssociations;
using Association.Application.Queries.GetAssociationByIdentifier;
using Association.Application.Queries.GetAssociationDetailsById;
using Association.Application.Queries.GetAssociationMembers;
using Association.Application.Queries.GetInvitationDetailsQuery;
using Association.Application.Queries.GetUserAssociationClaims;
using Association.Application.Services;
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
            // -----------------------------------------
            // Command handlers
            // -----------------------------------------

            // Create a new Association.
            services.AddScoped<
                ICommandHandler<CreateAssociationCommand, Guid>,
                CreateAssociationCommandHandler>();

            // Invite a member to an Association.
            services.AddScoped<
                ICommandHandler<InviteMemberCommand, Guid>,
                InviteMemberCommandHandler>();

            // Remove a member from an Association.
            services.AddScoped<
                ICommandHandler<RemoveMemberCommand, bool>,
                RemoveMemberCommandHandler>();

            // Accept an existing Association invitation.
            services.AddScoped<
                ICommandHandler<AcceptInvitationCommand, Guid>,
                AcceptInvitationCommandHandler>();


            // -----------------------------------------
            // Query handlers
            // -----------------------------------------

            // Retrieve all Associations.
            services.AddScoped<
                IQueryHandler<GetAllAssociationsQuery, IReadOnlyList<AssociationDto>>,
                GetAllAssociationsQueryHandler>();

            // Retrieve an Association by its identifier.
            services.AddScoped<
                IQueryHandler<GetAssociationByIdentifierQuery, AssociationDto?>,
                GetAssociationByIdentifierQueryHandler>();

            // Retrieve the current Association's details.
            services.AddScoped<
                IQueryHandler<GetCurrentAssociationDetailsQuery, AssociationDto?>,
                GetCurrentAssociationDetailsQueryHandler>();

            // Retrieve all Associations belonging to a user.
            services.AddScoped<
                IQueryHandler<GetAllAssociationsByUserQuery, IReadOnlyList<UserAssociationDto>>,
                GetAllAssociationsByUserQueryHandler>();

            // Retrieve members of an Association.
            services.AddScoped<
                IQueryHandler<GetAssociationMembersQuery, IReadOnlyList<AssociationMemberDto>>,
                GetAssociationMembersQueryHandler>();

            // Retrieve invitation details.
            services.AddScoped<
                IQueryHandler<GetInvitationDetailsQuery, InvitationDetailsDto?>,
                GetInvitationDetailsHandler>();

            // Retrieve tenant membership claims for a user.
            services.AddScoped<
                IQueryHandler<GetUserAssociationClaimsQuery, IReadOnlyCollection<Claim>>,
                GetUserAssociationClaimsQueryHandler>();


            // -----------------------------------------
            // Application services
            // -----------------------------------------

            // Checks whether an Association has capacity
            // for additional members based on its tier.
            services.AddScoped<AssociationMemberCapacityChecker>();


            // -----------------------------------------
            // Application context
            // -----------------------------------------

            // Provides validated information about the
            // current Association and current tenant user.
            services.AddScoped<ICurrentAssociation, CurrentAssociation>();


            return services;
        }
    }
}
