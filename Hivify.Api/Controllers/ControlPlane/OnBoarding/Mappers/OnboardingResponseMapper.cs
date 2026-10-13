using Hivify.Api.Controllers.ControlPlane.OnBoarding.Responses;
using OnBoarding.Application.DTOs;

namespace Hivify.Api.Controllers.ControlPlane.OnBoarding.Mappers;

public static class OnboardingResponseMapper
{
    public static CreatedOnboardingResponse ToCreatedResponse(StartOnboardingResult result)
    {
        return new CreatedOnboardingResponse(
            result.OnboardingId,
            result.Status.ToString());
    }
}
