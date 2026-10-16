using Hivify.Api.Controllers.ControlPlane.OnBoarding.Mappers;
using Hivify.Api.Controllers.ControlPlane.OnBoarding.Requests;
using Hivify.Api.Controllers.ControlPlane.OnBoarding.Responses;
using Identity.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnBoarding.Application.Contracts;
using OnBoarding.Domain.Enums;

namespace Hivify.Api.Controllers.ControlPlane.OnBoarding;

[ApiController]
[Route("api/onboarding")]
[Authorize]
public sealed class OnboardingController(
    IOnboardingService onboardingService,
    UserManager<DotNETApplicationUser> userManager,
    SignInManager<DotNETApplicationUser> signInManager) : ControllerBase
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
        {
            var user = await userManager.GetUserAsync(User);

            if (user is null)
                return Unauthorized();

            await signInManager.RefreshSignInAsync(user);
        }

        return CreatedAtAction(
            nameof(CreateOnboarding),
            new { id = result.OnboardingId },
            OnboardingResponseMapper.ToCreatedResponse(result));
    }
}