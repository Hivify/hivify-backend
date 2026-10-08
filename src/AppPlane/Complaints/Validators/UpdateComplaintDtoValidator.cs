using Complaints.Application.Contracts;
using FluentValidation;


namespace Complaints.Application.Validators
{
    public class UpdateComplaintDtoValidator : AbstractValidator<UpdateComplaint>
    {
        public UpdateComplaintDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Titel är obligatorisk.")
                .MaximumLength(200).WithMessage("Titeln får vara max 200 tecken.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Beskrivning är obligatorisk.")
                .MaximumLength(1000).WithMessage("Beskrivningen får vara max 1000 tecken.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Ogiltig status.");
        }
    }
}