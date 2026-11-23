using FluentValidation;

namespace Hivify.Api.Controllers.ControlPlane.Identity.Requests
{
    public sealed record RegisterUserReq(
        string Email,
        string Password,
        string FullName);

    public sealed class RegisterUserReqValidator : AbstractValidator<RegisterUserReq>
    {
        public RegisterUserReqValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
            RuleFor(x => x.FullName)
                .NotEmpty();
        }
    }
}