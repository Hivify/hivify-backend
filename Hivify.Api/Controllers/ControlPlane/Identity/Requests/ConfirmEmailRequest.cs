namespace Hivify.Api.Controllers.ControlPlane.Identity.Requests;

public sealed record ConfirmEmailRequest(
   string UserId,
   string Token);

