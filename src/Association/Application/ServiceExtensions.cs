using Association.Application.Commands.AddAssociation;
using Association.Application.Commands.AddStaffMember;
using Association.Application.Commands.RemoveStaffMember;
using Association.Application.Commands.UpdateStaffMemberRole;
using Association.Application.DTOs;
using Association.Application.Queries.GetAssociation.AllAssociations;
using Association.Application.Queries.GetAssociation.SingleAssociation;
using Association.Application.Queries.GetMember.AllMembers;
using Association.Application.Queries.GetMember.SingleMember;
using Association.Domain.Associations;
using Association.Domain.Members;
using BuildingBlocks.ApplicationPorts.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Association.Application;

public static class ServiceExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAssociationServices()
        {
            services.AddScoped<ICommandHandler<AddStaffMemberCommand, MemberID>, AddStaffMemberCommandHandler>();
            services.AddScoped<ICommandHandler<AddAssociationCommand, AssociationID>, CreateAssociationCommandHandler>();
            services.AddScoped<IQueryHandler<GetAssociationsQuery, IReadOnlyList<AssociationListItem>>, GetAssociationsQueryHandler>();
            services.AddScoped<IQueryHandler<GetAssociationQuery, AssociationListItem>, GetAssociationQueryHandler>();
            services.AddScoped<ICommandHandler<RemoveStaffMemberCommand, bool>, RemoveStaffMemberCommandHandler>();
            services.AddScoped<
    ICommandHandler<UpdateStaffMemberRoleCommand, bool>, UpdateStaffMemberRoleCommandHandler>();
            services.AddScoped<IQueryHandler<GetSingleMemberQuery, StaffMemberItem>, GetSingleMemberQueryHandler>();
            services.AddScoped<IQueryHandler<GetMembersQuery, IReadOnlyList<StaffMemberItem>>, GetMembersQueryHandler>();



            return services;
        }
    }
}





