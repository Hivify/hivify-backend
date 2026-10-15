using FluentValidation;
namespace Hivify.Api.Controllers.AppPlane.Houses.Requests
{
    public sealed record CreateHouseRequest(
     string Address,
     string HouseNumber,
     string PostalCode);

    public sealed class validator : AbstractValidator<CreateHouseRequest>
    {
        public validator()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(x => x.HouseNumber)
                .NotEmpty()
                .MaximumLength(10);
            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .Matches(@"^\d{5}(-\d{4})?$")
                .WithMessage("Postal code must be in the format '12345' or '12345-6789'.");
        }
    }
}
