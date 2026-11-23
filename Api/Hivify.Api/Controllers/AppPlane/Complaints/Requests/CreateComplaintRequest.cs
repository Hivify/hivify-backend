using FluentValidation;

namespace Hivify.Api.Controllers.AppPlane.Complaints.Requests
{

    public sealed record CreateComplaintRequest(
        Guid AssociationId,
        string Title,
        string Description,
        string? ImageUrl);

    public sealed class CreateComplaintRequestValidator : AbstractValidator<CreateComplaintRequest>
    {
        public CreateComplaintRequestValidator()
        {
            RuleFor(x => x.AssociationId)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.ImageUrl)
                .MaximumLength(1000)
                .When(x => x.ImageUrl is not null);
        }
    }
}




