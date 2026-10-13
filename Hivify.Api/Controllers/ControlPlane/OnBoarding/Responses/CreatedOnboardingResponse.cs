namespace Hivify.Api.Controllers.ControlPlane.OnBoarding.Responses;

public sealed record CreatedOnboardingResponse(
    Guid OnboardingId,
    string Status);
