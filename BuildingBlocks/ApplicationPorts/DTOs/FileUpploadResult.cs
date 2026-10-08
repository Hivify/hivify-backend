namespace BuildingBlocks.ApplicationPorts.DTOs
{
    public sealed record FileUploadResult(
      string PublicId,
      string Url,
      string SecureUrl);
}
