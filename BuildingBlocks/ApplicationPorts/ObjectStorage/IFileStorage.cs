using BuildingBlocks.ApplicationPorts.ObjectStorage.DTOs;

namespace BuildingBlocks.ApplicationPorts.ObjectStorage
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
