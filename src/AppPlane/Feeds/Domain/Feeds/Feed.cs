using SharedKernel;
using SharedKernel.Exceptions;
using SharedKernel.ValuesObjects;

namespace Feeds.Domain.Feeds;

public class Feed : BaseEntity<FeedID>, IAggregateRoot
{
    public Title Title { get; private set; }

    public Description Content { get; private set; }

    public UserID AuthorId { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    private Feed()
    {
    }

    private Feed(
        FeedID id,
        UserID authorId,
        Title title,
        Description content) : base(id)
    {
        CreatedDate = DateTime.UtcNow;
        AuthorId = authorId;

        SetTitle(title);
        SetContent(content);
    }

    public static Feed CreateFeed(
        UserID authorId,
        Title title,
        Description content)
    {
        return new Feed(
            new FeedID(Guid.NewGuid()),
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