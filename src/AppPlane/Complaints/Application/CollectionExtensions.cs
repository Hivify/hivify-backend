using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Complaints.Application.Commands.CreateComplaint;
using Complaints.Application.Commands.UpdateComplaintStatus;
using Complaints.Application.DTOs;
using Complaints.Application.Queries.GetComplaint;
using Microsoft.Extensions.DependencyInjection;

namespace Complaints.Application;

public static class CollectionExtensions
{

    extension(IServiceCollection services)
    {

        public IServiceCollection AddComplaintsApplication()
        {
            // Commands
            services.AddScoped<ICommandHandler<CreateComplaintCommand, Guid>, CreateComplaintCommandHandler>();
            services.AddScoped<IQueryHandler<GetComplaintByIdQuery, ComplaintListDto?>, GetComplaintByIdQueryHandler>();
            services.AddScoped<ICommandHandler<UpdateComplaintStatusCommand, bool>, UpdateComplaintStatusCommandHandler>();

            // Queries
            services.AddScoped<IQueryHandler<GetUserComplaintsQuery, IReadOnlyList<ComplaintListDto>>, GetUserComplaintsQueryHandler>();
            services.AddScoped<IQueryHandler<GetAllComplaintsQuery, IReadOnlyList<ComplaintListDto>>, GetAllComplaintsQueryHandler>();

            return services;
        }

    }

}