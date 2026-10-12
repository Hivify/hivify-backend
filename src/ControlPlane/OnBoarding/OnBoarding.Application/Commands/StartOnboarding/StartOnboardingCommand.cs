using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using OnBoarding.Application.DTOs;

namespace OnBoarding.Application.Commands.StartOnboarding;

public sealed record StartOnboardingCommand : ICommand<StartOnboardingResult>;
