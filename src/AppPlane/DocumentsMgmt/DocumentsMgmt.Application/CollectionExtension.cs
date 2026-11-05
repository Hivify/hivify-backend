using Application.Commands;
using Application.Queries;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Storage.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

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