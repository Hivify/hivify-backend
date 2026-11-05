using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Application.Commands
{
    public sealed record UploadDocumentCommand(
        string FileName,
        string ContentType,
        Stream Content
    ) : ICommand<string>;
}
