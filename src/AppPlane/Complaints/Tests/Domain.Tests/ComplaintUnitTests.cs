using Complaints.Domain;
using SharedKernel.Exceptions;
using SharedKernel.ValueObjects;
using Xunit;

namespace Domain.Tests;

public class ComplaintTests
{
    private static Complaint CreateValidComplaint(string? imageUrl = null)
    {
        return Complaint.Create(
            new UserID(Guid.NewGuid()),
            new DepartmentID(Guid.NewGuid()),
            new Title("Test title"),
            new Description("Test description"),
            imageUrl);
    }

    // ---------- Create ----------

    [Fact]
    public void Create_WithValidData_ShouldSetProperties()
    {
        var userId = new UserID(Guid.NewGuid());
        var associationId = new DepartmentID(Guid.NewGuid());
        var title = new Title("Broken door");
        var description = new Description("The door does not close");

        var before = DateTime.UtcNow;

        var complaint = Complaint.Create(
            userId, associationId, title, description, "https://img/1.png");

        var after = DateTime.UtcNow;

        Assert.NotEqual(Guid.Empty, complaint.Id.Value);
        Assert.Equal(userId, complaint.UserId);
        Assert.Equal(associationId, complaint.AssociationId);
        Assert.Equal(title, complaint.Title);
        Assert.Equal(description, complaint.Description);
        Assert.Equal("https://img/1.png", complaint.ImageUrl);
        Assert.Equal(ComplaintStatus.New, complaint.Status);
        Assert.InRange(complaint.CreatedDate, before, after);
        Assert.Null(complaint.UpdatedDate);
        Assert.Null(complaint.ResolvedDate);
        Assert.Null(complaint.AdminComment);
    }

    [Fact]
    public void Create_WithoutImage_ShouldHaveNullImageUrl()
    {
        var complaint = CreateValidComplaint();

        Assert.Null(complaint.ImageUrl);
    }

    [Fact]
    public void Create_CalledTwice_ShouldGenerateDifferentIds()
    {
        var first = CreateValidComplaint();
        var second = CreateValidComplaint();

        Assert.NotEqual(first.Id.Value, second.Id.Value);
    }

    // ---------- UpdateDetails ----------

    [Fact]
    public void UpdateDetails_WithValidData_ShouldUpdatePropertiesAndUpdatedDate()
    {
        var complaint = CreateValidComplaint();
        var newTitle = new Title("New title");
        var newDescription = new Description("New description");

        var before = DateTime.UtcNow;
        complaint.UpdateDetails(newTitle, newDescription);
        var after = DateTime.UtcNow;

        Assert.Equal(newTitle, complaint.Title);
        Assert.Equal(newDescription, complaint.Description);
        Assert.NotNull(complaint.UpdatedDate);
        Assert.InRange(complaint.UpdatedDate!.Value, before, after);
    }

    [Fact]
    public void UpdateDetails_ShouldNotChangeStatusOrCreatedDate()
    {
        var complaint = CreateValidComplaint();
        var createdDate = complaint.CreatedDate;

        complaint.UpdateDetails(new Title("A"), new Description("B"));

        Assert.Equal(ComplaintStatus.New, complaint.Status);
        Assert.Equal(createdDate, complaint.CreatedDate);
    }

    // Only works if Title/Description are reference types (classes).
    // If they are structs, `?? throw` can never trigger, see notes below.
    [Fact]
    public void UpdateDetails_WithNullTitle_ShouldThrowDomainException()
    {
        var complaint = CreateValidComplaint();

        var action = () => complaint.UpdateDetails(null!, new Description("B"));

        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void UpdateDetails_WithNullDescription_ShouldThrowDomainException()
    {
        var complaint = CreateValidComplaint();

        var action = () => complaint.UpdateDetails(new Title("A"), null!);

        Assert.Throws<DomainException>(action);
    }

    // ---------- SetImage ----------

    [Fact]
    public void SetImage_WithValidUrl_ShouldSetImageAndUpdatedDate()
    {
        var complaint = CreateValidComplaint();

        var before = DateTime.UtcNow;
        complaint.SetImage("https://img/2.png");
        var after = DateTime.UtcNow;

        Assert.Equal("https://img/2.png", complaint.ImageUrl);
        Assert.InRange(complaint.UpdatedDate!.Value, before, after);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SetImage_WithInvalidUrl_ShouldThrowAndNotChangeState(string? url)
    {
        var complaint = CreateValidComplaint("https://img/original.png");

        var action = () => complaint.SetImage(url!);

        Assert.Throws<DomainException>(action);
        Assert.Equal("https://img/original.png", complaint.ImageUrl);
        Assert.Null(complaint.UpdatedDate);
    }

    // ---------- UpdateStatus ----------

    [Fact]
    public void UpdateStatus_ToUnderTreatment_ShouldSetStatusWithoutResolvedDate()
    {
        var complaint = CreateValidComplaint();

        complaint.UpdateStatus(ComplaintStatus.UnderTreatment);

        Assert.Equal(ComplaintStatus.UnderTreatment, complaint.Status);
        Assert.NotNull(complaint.UpdatedDate);
        Assert.Null(complaint.ResolvedDate);
    }

    [Fact]
    public void UpdateStatus_ToResolved_ShouldSetResolvedDate()
    {
        var complaint = CreateValidComplaint();

        var before = DateTime.UtcNow;
        complaint.UpdateStatus(ComplaintStatus.Resolved);
        var after = DateTime.UtcNow;

        Assert.Equal(ComplaintStatus.Resolved, complaint.Status);
        Assert.NotNull(complaint.ResolvedDate);
        Assert.InRange(complaint.ResolvedDate!.Value, before, after);
    }

    [Fact]
    public void UpdateStatus_FullLifecycle_ShouldEndResolved()
    {
        var complaint = CreateValidComplaint();

        complaint.UpdateStatus(ComplaintStatus.UnderTreatment);
        complaint.UpdateStatus(ComplaintStatus.Resolved);

        Assert.Equal(ComplaintStatus.Resolved, complaint.Status);
        Assert.NotNull(complaint.ResolvedDate);
    }

    // ---------- AddAdminComment ----------

    [Fact]
    public void AddAdminComment_WithValidComment_ShouldSetTrimmedCommentAndUpdatedDate()
    {
        var complaint = CreateValidComplaint();

        complaint.AddAdminComment("  We are looking into it  ");

        Assert.Equal("We are looking into it", complaint.AdminComment);
        Assert.NotNull(complaint.UpdatedDate);
    }

    [Fact]
    public void AddAdminComment_CalledTwice_ShouldReplacePreviousComment()
    {
        var complaint = CreateValidComplaint();

        complaint.AddAdminComment("First");
        complaint.AddAdminComment("Second");

        Assert.Equal("Second", complaint.AdminComment);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void AddAdminComment_WithInvalidComment_ShouldThrowAndNotChangeState(string? comment)
    {
        var complaint = CreateValidComplaint();

        var action = () => complaint.AddAdminComment(comment!);

        Assert.Throws<DomainException>(action);
        Assert.Null(complaint.AdminComment);
        Assert.Null(complaint.UpdatedDate);
    }
}