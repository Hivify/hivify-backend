using Hivify.Api.Controllers.ControlPlane.OnBoarding.Mappers;
using Hivify.Api.Controllers.ControlPlane.OnBoarding.Requests;
using Hivify.Api.Controllers.ControlPlane.OnBoarding.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnBoarding.Application.Contracts;

namespace Hivify.Api.Controllers.ControlPlane.OnBoarding;

[ApiController]
[Route("api/onboarding")]
[Authorize]
public sealed class OnboardingController(IOnboardingService onboardingService) : ControllerBase
{
    private readonly IOnboardingService _onboardingService = onboardingService;

    [HttpPost]
    public async Task<ActionResult<CreatedOnboardingResponse>> CreateOnboarding(
        CreateOnboardingRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _onboardingService.StartAsync(request.AssociationName, cancellationToken);

        return CreatedAtAction(
            nameof(CreateOnboarding),
            new { id = result.OnboardingId },
            OnboardingResponseMapper.ToCreatedResponse(result));
    }
}
