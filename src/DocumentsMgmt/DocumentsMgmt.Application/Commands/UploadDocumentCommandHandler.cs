using BuildingBlocks.ApplicationPorts.Messaging;
using BuildingBlocks.ApplicationPorts.Storage;

namespace DocumentsMgmt.Application.Commands
{
    public sealed class UploadDocumentCommandHandler : ICommandHandler<UploadDocumentCommand, string>
    {
        private readonly IFileStorage _fileStorage;

        public UploadDocumentCommandHandler(
            IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

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