using Department.Domain.Departments;
using Department.Domain.Members;
using SharedKernel.Exceptions;
using SharedKernel.ValueObjects;
using Xunit;

namespace Domain.Tests;

public class MemberUnitTests
{
    private static readonly UserID DefaultUserId = new(Guid.NewGuid());

    private static DepartmentEntity CreateValidDepartment()
    {
        return DepartmentEntity.Create(
            new Name("Test Department"));
    }

    private static Member AddMember(
        DepartmentEntity department,
        UserID? userId = null,
        MemberRole role = MemberRole.CommiteAnsvarig)
    {
        return department.CreateMember(
            userId ?? new UserID(Guid.NewGuid()),
            new Name("Test Member"),
            new Email("test@example.com"),
            role);
    }

    [Fact]
    public void CreateMember_WithValidData_ShouldAddMemberToDepartment()
    {
        var department = CreateValidDepartment();
        var userId = new UserID(Guid.NewGuid());

        var member = AddMember(department, userId);

        Assert.Single(department.StaffMembers);
        Assert.Contains(member, department.StaffMembers);
        Assert.Equal(userId, member.UserId);
    }

    [Fact]
    public void CreateMember_WithSameUserTwice_ShouldThrowDomainException()
    {
        var department = CreateValidDepartment();
        var userId = new UserID(Guid.NewGuid());
        AddMember(department, userId);

        var action = () => AddMember(department, userId);

        Assert.Throws<DomainException>(action);
        Assert.Single(department.StaffMembers);
    }

    [Fact]
    public void CreateMember_WithDifferentUsers_ShouldAddBoth()
    {
        var department = CreateValidDepartment();

        AddMember(department);
        AddMember(department);

        Assert.Equal(2, department.StaffMembers.Count);
    }

    [Fact]
    public void CreateMember_WhenSameUserWasPreviouslyRemoved_ShouldSucceed()
    {
        var department = CreateValidDepartment();
        var userId = new UserID(Guid.NewGuid());
        var first = AddMember(department, userId);
        department.RemoveMember(first);

        var second = AddMember(department, userId);

        Assert.NotEqual(first.Id, second.Id);
    }

    [Fact]
    public void UpdateMemberRole_WithExistingMember_ShouldChangeRole()
    {
        var department = CreateValidDepartment();
        var member = AddMember(department, role: MemberRole.CommiteAnsvarig);

        department.UpdateMemberRole(member.Id, MemberRole.StyrelseLedamot);

        Assert.Equal(MemberRole.StyrelseLedamot, member.Role);
    }

    [Fact]
    public void UpdateMemberRole_WithUnknownMemberId_ShouldThrowDomainException()
    {
        var department = CreateValidDepartment();

        var action = () => department.UpdateMemberRole(
            new MemberID(Guid.NewGuid()),
            MemberRole.StyrelseLedamot);

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void UpdateMemberRole_WhenMemberIsRemoved_ShouldThrowDomainException()
    {
        var department = CreateValidDepartment();
        var member = AddMember(department);
        department.RemoveMember(member);

        var action = () => department.UpdateMemberRole(
            member.Id,
            MemberRole.StyrelseLedamot);

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void RemoveMember_WithExistingMember_ShouldSoftDeleteMember()
    {
        var department = CreateValidDepartment();
        var member = AddMember(department);

        department.RemoveMember(member);

        Assert.NotNull(member.DeletedAt);
    }

    [Fact]
    public void RemoveMember_WithMemberFromAnotherDepartment_ShouldThrowDomainException()
    {
        var department = CreateValidDepartment();
        var otherDepartment = CreateValidDepartment();
        var outsider = AddMember(otherDepartment);

        var action = () => department.RemoveMember(outsider);

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void RemoveMember_WhenAlreadyRemoved_ShouldThrowDomainException()
    {
        var department = CreateValidDepartment();
        var member = AddMember(department);
        department.RemoveMember(member);

        var action = () => department.RemoveMember(member);

        Assert.Throws<DomainException>(action);
    }
}
