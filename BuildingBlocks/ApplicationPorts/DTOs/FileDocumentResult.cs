namespace BuildingBlocks.ApplicationPorts.DTOs
{
    public sealed record FileDocumentResult(
      string PublicId,
      string FileName,
      string Url,
      string SecureUrl);
}
