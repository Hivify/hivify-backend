using FluentValidation;
namespace Hivify.Api.Controllers.AppPlane.Houses.Requests
{
    public sealed record AddTenantRequest(
     Guid UserId,
     string Email,
     string FullName,
     string PhoneNumber);

    public sealed class AddTenantRequestValidator : AbstractValidator<AddTenantRequest>
    {
        public AddTenantRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be in E.164 format.");
        }
    }
}
