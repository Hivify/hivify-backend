using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Storage.DTOs;

namespace Application.Queries
{
    public sealed record GetDocumentsQuery : IQuery<IReadOnlyList<FileDocumentResult>>;
}
