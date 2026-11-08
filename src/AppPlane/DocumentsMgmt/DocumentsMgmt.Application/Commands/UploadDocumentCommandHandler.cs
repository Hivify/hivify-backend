using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Storage;

namespace DocumentsMgmt.Application.Commands
{
    public sealed class UploadDocumentCommandHandler(
        IFileStorage fileStorage) : ICommandHandler<UploadDocumentCommand, string>
    {
        private readonly IFileStorage _fileStorage = fileStorage;

        public async Task<string> Handle(
            UploadDocumentCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _fileStorage.UploadAsync(
                command.Content,
                command.FileName,
                command.ContentType,
                cancellationToken);

            return result.PublicId;
        }

    }
}