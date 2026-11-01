using BuildingBlocks.ApplicationPorts.HivifyUser;
using Hivify.Api.Controllers.ControlPlane.OnBoarding.Mappers;
using Hivify.Api.Controllers.ControlPlane.OnBoarding.Requests;
using Hivify.Api.Controllers.ControlPlane.OnBoarding.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnBoarding.Application.Contracts;
using OnBoarding.Domain.Enums;

namespace Hivify.Api.Controllers.ControlPlane.OnBoarding;

[ApiController]
[Route("api/onboarding")]
[Authorize]
public sealed class OnboardingController(
    IOnboardingService onboardingService,
    IUserSignInRefresher signInRefresher) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreatedOnboardingResponse>> CreateOnboarding(
        CreateOnboardingRequest request,
        CancellationToken ct)
    {
        var result = await onboardingService.StartOnboardingAsync(
            request.AssociationName,
            ct);

        if (result.Status == OnboardingStatus.Completed)
            await signInRefresher.RefreshAsync();

        return CreatedAtAction(
            nameof(CreateOnboarding),
            new { id = result.OnboardingId },
            OnboardingResponseMapper.ToCreatedResponse(result));
    }
}