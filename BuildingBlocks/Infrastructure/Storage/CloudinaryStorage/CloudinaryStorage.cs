using BuildingBlocks.ApplicationPorts.Storage;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace BuildingBlocks.Infrastructure.Storage.CloudinaryStorage
{
    public sealed class CloudinaryFileStorage : IFileStorage
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryFileStorage(
            IOptions<CloudinaryOptions> options)
        {
            var settings = options.Value;

            var account = new Account(
                settings.CloudName,
                settings.ApiKey,
                settings.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }

        public async Task<FileUploadResult> UploadAsync(
            Stream content,
            string fileName,
            string contentType,
            CancellationToken cancellationToken = default)
        {
            var file = new FileDescription(
                fileName,
                content);

            var uploadParams = new RawUploadParams
            {
                File = file,
                Folder = "ansjon/documents"
            };

            var result = await _cloudinary.UploadAsync(
                uploadParams,
                "raw",
                cancellationToken);

            if (result.Error is not null)
            {
                throw new InvalidOperationException(
                    $"Cloudinary upload failed: {result.Error.Message}");
            }

            return new FileUploadResult(
                result.PublicId,
                result.Url?.ToString() ?? string.Empty,
                result.SecureUrl?.ToString() ?? string.Empty);
        }
        public async Task<IReadOnlyList<FileDocumentResult>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var listParams = new ListResourcesParams
            {
                ResourceType = CloudinaryDotNet.Actions.ResourceType.Raw,
                Type = "upload",
                MaxResults = 100
            };

            var result = await _cloudinary.ListResourcesAsync(listParams);

            if (result.Error is not null)
            {
                throw new InvalidOperationException(
                    $"Cloudinary list failed: {result.Error.Message}");
            }

            return result.Resources
                .Select(resource => new FileDocumentResult(
                    resource.PublicId,
                    resource.PublicId.Split('/').Last(),
                    resource.Url?.ToString() ?? string.Empty,
                    resource.SecureUrl?.ToString() ?? string.Empty))
                .ToList();
        }
    }
}
