using BuildingBlocks.ApplicationPorts.Storage.DTOs;

namespace BuildingBlocks.ApplicationPorts.Storage
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
