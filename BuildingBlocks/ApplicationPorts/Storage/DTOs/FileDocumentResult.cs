namespace BuildingBlocks.ApplicationPorts.Storage.DTOs
{
    public sealed record FileDocumentResult(
      string PublicId,
      string FileName,
      string Url,
      string SecureUrl);
}
