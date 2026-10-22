using Feeds.Domain.ValueObjects;
using SharedKernel;
using SharedKernel.Exceptions;
using SharedKernel.ValueObjects;

namespace Feeds.Domain.Feeds;

public class Feed : BaseEntity<FeedID>, IAggregateRoot
{
    public Title Title { get; private set; }

    public Description Content { get; private set; }

    public UserID AuthorId { get; private set; }

    public AssociationID AssociationId { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    private Feed()
    {
    }

    private Feed(
    FeedID id,
    AssociationID associationId,
    UserID authorId,
    Title title,
    Description content) : base(id)
    {
        if (associationId.Value == Guid.Empty)
        {
            throw new DomainException(
                "A feed must belong to an association.");
        }

        AssociationId = associationId;
        AuthorId = authorId;
        CreatedDate = DateTime.UtcNow;

        SetTitle(title);
        SetContent(content);
    }

    public static Feed CreateFeed(
    AssociationID associationId,
    UserID authorId,
    Title title,
    Description content)
    {
        return new Feed(
            new FeedID(Guid.NewGuid()),
            associationId,
            authorId,
            title,
            content);
    }

    public void Update(
        Title title,
        Description content)
    {
        EnsureNotDeleted();

        SetTitle(title);
        SetContent(content);
    }

    public void Delete()
    {
        EnsureNotDeleted();

        DeletedAt = DateTime.UtcNow;
    }

    private void SetTitle(Title title)
    {
        Title = title;
    }

    private void SetContent(Description content)
    {
        Content = content;
    }

    private void EnsureNotDeleted()
    {
        if (DeletedAt != null)
        {
            throw new DomainException(
                "Feed has already been deleted.");
        }
    }
}