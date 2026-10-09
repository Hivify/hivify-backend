using Feeds.Domain.Feeds;
using SharedKernel.Exceptions;
using SharedKernel.ValueObjects;

namespace Feeds.Domain.Tests.Feeds;

public class FeedTests
{
    private static Feed CreateValidFeed()
    {
        return Feed.CreateFeed(
            new UserID(Guid.NewGuid()),
            new Title("Test title"),
            new Description("Test content"));
    }

    [Fact]
    public void CreateFeed_WithValidData_ShouldSetProperties()
    {
        // Arrange
        var authorId = new UserID(Guid.NewGuid());
        var title = new Title("Test title");
        var content = new Description("Test content");

        // Act
        var feed = Feed.CreateFeed(
            authorId,
            title,
            content);

        // Assert
        Assert.NotEqual(Guid.Empty, feed.Id.Value);
        Assert.Equal(authorId, feed.AuthorId);
        Assert.Equal(title, feed.Title);
        Assert.Equal(content, feed.Content);
        Assert.Null(feed.DeletedAt);
    }

    [Fact]
    public void UpdateFeed_WithValidData_ShouldUpdateProperties()
    {
        // Arrange
        var feed = CreateValidFeed();

        var newTitle = new Title("Updated title");
        var newContent = new Description("Updated content");

        // Act
        feed.Update(
            newTitle,
            newContent);

        // Assert
        Assert.Equal(newTitle, feed.Title);
        Assert.Equal(newContent, feed.Content);
    }

    [Fact]
    public void DeleteFeed_ShouldSetDeletedAt()
    {
        // Arrange
        var feed = CreateValidFeed();

        var before = DateTime.UtcNow;

        // Act
        feed.Delete();

        var after = DateTime.UtcNow;

        // Assert
        Assert.NotNull(feed.DeletedAt);

        Assert.InRange(
            feed.DeletedAt!.Value,
            before,
            after);
    }

    [Fact]
    public void UpdateFeed_WhenDeleted_ShouldThrowDomainException()
    {
        // Arrange
        var feed = CreateValidFeed();

        feed.Delete();

        var newTitle = new Title("Updated title");
        var newContent = new Description("Updated content");

        // Act
        var action = () => feed.Update(
            newTitle,
            newContent);

        // Assert
        Assert.Throws<DomainException>(action);
    }

    [Fact]
    public void DeleteFeed_WhenAlreadyDeleted_ShouldThrowDomainException()
    {
        // Arrange
        var feed = CreateValidFeed();

        feed.Delete();

        // Act
        var action = () => feed.Delete();

        // Assert
        Assert.Throws<DomainException>(action);
    }
}