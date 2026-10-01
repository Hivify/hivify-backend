using FluentValidation;
namespace Hivify.Api.Controllers.Departments.Requests
{
    public sealed record CreateDepartmentReq(
    string Name);



    public sealed class CreateDepartmentReqValidator : AbstractValidator<CreateDepartmentReq>
    {
        public CreateDepartmentReqValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }

}

