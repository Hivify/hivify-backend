using SharedKernel;
using SharedKernel.Exceptions;
using SharedKernel.ValueObjects;

namespace Complaints.Domain
{
    public class Complaint : BaseEntity<ComplaintID>, IAggregateRoot
    {
        public UserID UserId { get; private set; }
        public DepartmentID DepartmentId { get; private set; }
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public string? ImageUrl { get; private set; }
        public ComplaintStatus Status { get; private set; }
        public DateTimeOffset CreatedDate { get; private set; }
        public DateTimeOffset? UpdatedDate { get; private set; }
        public DateTimeOffset? ResolvedDate { get; private set; }
        public string? AdminComment { get; private set; }

        private Complaint() { }

        private Complaint(
            ComplaintID id,
            UserID userId,
            DepartmentID departmentId,
            Title title,
            Description description,
            string? imageUrl)
            : base(id)
        {
            UserId = userId;
            DepartmentId = departmentId;
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
            Status = ComplaintStatus.New;
            CreatedDate = DateTimeOffset.UtcNow;
            DepartmentId = departmentId;
        }

        public static Complaint Create(
            UserID userId,
            DepartmentID departmentId,
            Title title,
            Description description,
            string? imageUrl = null)
        {
            return new Complaint(
                new ComplaintID(Guid.NewGuid()),
                userId,
                departmentId,
                title,
                description,
                imageUrl);
        }

        public void UpdateDetails(Title title, Description description)
        {
            Title = title ?? throw new DomainException("You must provide a title.");
            Description = description ?? throw new DomainException("You must provide a description.");
            UpdatedDate = DateTimeOffset.UtcNow;
        }

        public void SetImage(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
                throw new DomainException("You must provide an image URL.");
            ImageUrl = imageUrl;
            UpdatedDate = DateTimeOffset.UtcNow;
        }

        public void UpdateStatus(ComplaintStatus newStatus)
        {
            Status = newStatus;
            UpdatedDate = DateTimeOffset.UtcNow;

            if (newStatus == ComplaintStatus.Resolved)
            {
                ResolvedDate = DateTimeOffset.UtcNow;
            }
        }

        public void AddAdminComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new DomainException("You must provide an admin comment.");
            AdminComment = comment.Trim();
            UpdatedDate = DateTimeOffset.UtcNow;
        }
    }
}