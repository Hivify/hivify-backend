namespace BuildingBlocks.ApplicationPorts.Storage.DTOs
{
    public sealed record FileUploadResult(
      string PublicId,
      string Url,
      string SecureUrl);
}
