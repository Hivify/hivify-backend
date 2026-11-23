using FluentValidation;
namespace Hivify.Api.Controllers.AppPlane.Feeds.Requests
{

    public sealed record CreateFeedRequest(
    string Title,
    string Description);


    public sealed class CreateFeedRequestValidator : AbstractValidator<CreateFeedRequest>
    {
        public CreateFeedRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);
        }
    }


}

