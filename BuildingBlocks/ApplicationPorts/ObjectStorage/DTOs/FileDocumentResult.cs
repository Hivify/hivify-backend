namespace BuildingBlocks.ApplicationPorts.ObjectStorage.DTOs
{
    public sealed record FileDocumentResult(
      string PublicId,
      string FileName,
      string Url,
      string SecureUrl);
}
