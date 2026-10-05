using BuildingBlocks.ApplicationPorts.Messaging;
using Department.Application.Commands.AddDepartment;
using Department.Application.Commands.AddStaffMember;
using Department.Application.Commands.RemoveStaffMember;
using Department.Application.Commands.UpdateStaffMemberRole;
using Department.Application.DTOs;
using Department.Application.Queries.GetDepartment.AllDepartments;
using Department.Application.Queries.GetDepartment.SingleDepartment;
using Department.Application.Queries.GetMember.AllMembers;
using Department.Application.Queries.GetMember.SingleMember;
using Department.Domain.Departments;
using Department.Domain.Members;
using Microsoft.Extensions.DependencyInjection;

namespace Department.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddDepartmentServices(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<AddStaffMemberCommand, MemberID>, AddStaffMemberCommandHandler>();
        services.AddScoped<ICommandHandler<AddDepartmentCommand, DepartmentID>, CreateDepartmentCommandHandler>();
        services.AddScoped<IQueryHandler<GetDepartmentsQuery, IReadOnlyList<DepartmentListItem>>, GetDepartmentsQueryHandler>();
        services.AddScoped<IQueryHandler<GetDepartmentQuery, DepartmentListItem>, GetDepartmentQueryHandler>();
        services.AddScoped<ICommandHandler<RemoveStaffMemberCommand, bool>, RemoveStaffMemberCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateStaffMemberRoleCommand, bool>, UpdateStaffMemberRoleCommandHandler>();
        services.AddScoped<IQueryHandler<GetSingleMemberQuery, StaffMemberItem>, GetSingleMemberQueryHandler>();
        services.AddScoped<IQueryHandler<GetMembersQuery, IReadOnlyList<StaffMemberItem>>, GetMembersQueryHandler>();

        return services;
    }
}
