
using OnBoarding.Domain.Enums;

namespace OnBoarding.Application.DTOs;

public sealed record StartOnboardingResult(Guid OnboardingId, OnboardingStatus Status);
