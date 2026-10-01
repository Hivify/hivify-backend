using Department.Domain.Members;
using FluentValidation;
namespace Hivify.Api.Controllers.Departments.Requests
{
    public sealed record CreateDepartmentMemberReq(
     Guid UserId,
     string FullName,
    string Email,
    MemberRole Role
     );


    public sealed class CreateDepartmentMemberReqValidator : AbstractValidator<CreateDepartmentMemberReq>
    {
        public CreateDepartmentMemberReqValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100);
        }
    }
}
