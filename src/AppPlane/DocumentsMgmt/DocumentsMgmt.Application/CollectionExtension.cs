

using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using BuildingBlocks.ApplicationPorts.DTOs;
using DocumentsMgmt.Application.Commands;
using DocumentsMgmt.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentsMgmt.Application;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddDocumentServices()
        {
            services.AddScoped<ICommandHandler<UploadDocumentCommand, string>, UploadDocumentCommandHandler>();
            services.AddScoped<IQueryHandler<GetDocumentsQuery, IReadOnlyList<FileDocumentResult>>, GetDocumentsQueryHandler>();

            return services;
        }
    }
}