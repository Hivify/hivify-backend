using FluentValidation;

namespace Hivify.Api.Controllers.AppPlane.Complaints.Requests
{
    public sealed record UpdateComplaintStatusRequest(
     string Status,
     string? AdminComment);


    public sealed class UpdateComplaintStatusRequestValidator
    : AbstractValidator<UpdateComplaintStatusRequest>
    {
        public UpdateComplaintStatusRequestValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty()
                .MaximumLength(50)
                .Must(status => status == "Ny" || status == "Under behandling" || status == "Löst")
                .WithMessage("Status måste vara 'Ny', 'Under behandling' eller 'Löst'.");

            RuleFor(x => x.AdminComment)
                .MaximumLength(2000)
                .When(x => x.AdminComment is not null);
        }
    }
}
