namespace BuildingBlocks.ApplicationPorts.ObjectStorage.DTOs
{
    public sealed record FileUploadResult(
      string PublicId,
      string Url,
      string SecureUrl);
}
