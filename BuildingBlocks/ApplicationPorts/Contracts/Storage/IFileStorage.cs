using BuildingBlocks.ApplicationPorts.DTOs;

namespace BuildingBlocks.ApplicationPorts.Contracts.Storage
{
    public interface IFileStorage
    {
        Task<FileUploadResult> UploadAsync(
            Stream stream,
            string fileName,
            string contentType,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyList<FileDocumentResult>> GetAllAsync(CancellationToken cancellationToken = default);
    }


}
