using BuildingBlocks.ApplicationPorts.Messaging;
using Complaints.Application.Commands.CreateComplaint;
using Complaints.Application.Commands.UpdateComplaintStatus;
using Complaints.Application.DTOs;
using Complaints.Application.Queries.GetComplaint;
using Microsoft.Extensions.DependencyInjection;

namespace Complaints.Application;

public static class CollectionExtensions
{
    public static IServiceCollection AddComplaintServices(this IServiceCollection services)
    {
        // Commands
        services.AddScoped<ICommandHandler<CreateComplaintCommand, Guid>, CreateComplaintCommandHandler>();
        services.AddScoped<IQueryHandler<GetComplaintByIdQuery, ComplaintListItem?>, GetComplaintByIdQueryHandler>();
        services.AddScoped<ICommandHandler<UpdateComplaintStatusCommand, bool>, UpdateComplaintStatusCommandHandler>();

        // Queries
        services.AddScoped<IQueryHandler<GetUserComplaintsQuery, IReadOnlyList<ComplaintListItem>>, GetUserComplaintsQueryHandler>();
        services.AddScoped<IQueryHandler<GetAllComplaintsQuery, IReadOnlyList<ComplaintListItem>>, GetAllComplaintsQueryHandler>();



        return services;
    }
}