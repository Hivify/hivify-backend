using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using BuildingBlocks.ApplicationPorts.DTOs;

namespace DocumentsMgmt.Application.Queries
{
    public sealed record GetDocumentsQuery : IQuery<IReadOnlyList<FileDocumentResult>>;
}
