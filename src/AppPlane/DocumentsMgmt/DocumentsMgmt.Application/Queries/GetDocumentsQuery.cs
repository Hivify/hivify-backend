using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Storage.DTOs;

namespace DocumentsMgmt.Application.Queries
{
    public sealed record GetDocumentsQuery : IQuery<IReadOnlyList<FileDocumentResult>>;
}
