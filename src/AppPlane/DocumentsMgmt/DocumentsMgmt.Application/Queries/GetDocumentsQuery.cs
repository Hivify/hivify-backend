using BuildingBlocks.ApplicationPorts.Messaging;
using BuildingBlocks.ApplicationPorts.Storage.DTOs;

namespace DocumentsMgmt.Application.Queries
{
    public sealed record GetDocumentsQuery : IQuery<IReadOnlyList<FileDocumentResult>>;
}
