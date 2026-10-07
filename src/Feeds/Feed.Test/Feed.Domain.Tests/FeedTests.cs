using Feeds.Domain.Feeds;
using SharedKernel.Exceptions;
using SharedKernel.ValuesObjects;

namespace Feeds.Domain.Tests;

public class FeedTests
{
    [Fact]
    public void CreateFeed_WithValidData_ShouldCreateFeed()
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
        Assert.NotNull(feed);
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
        var authorId = new UserID(Guid.NewGuid());
        var title = new Title("Test title");
        var content = new Description("Test content");
        var feed = Feed.CreateFeed(
            authorId,
            title,
            content);
        var newTitle = new Title("Updated title");
        var newContent = new Description("Updated content");
        // Act
        feed.Update(newTitle, newContent);
        // Assert
        Assert.Equal(newTitle, feed.Title);
        Assert.Equal(newContent, feed.Content);
    }

    [Fact]
    public void DeleteFeed_ShouldSetDeletedAt()
    {
        // Arrange
        var authorId = new UserID(Guid.NewGuid());
        var title = new Title("Test title");
        var content = new Description("Test content");
        var feed = Feed.CreateFeed(
            authorId,
            title,
            content);
        // Act
        feed.Delete();
        // Assert
        Assert.NotNull(feed.DeletedAt);
    }


    [Fact]
    public void UpdateFeed_WhenDeleted_ShouldThrowDomainException()
    {
        // Arrange
        var authorId = new UserID(Guid.NewGuid());
        var title = new Title("Test title");
        var content = new Description("Test content");

        var feed = Feed.CreateFeed(
            authorId,
            title,
            content);

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
        var authorId = new UserID(Guid.NewGuid());
        var title = new Title("Test title");
        var content = new Description("Test content");

        var feed = Feed.CreateFeed(
            authorId,
            title,
            content);

        feed.Delete();

        // Act
        var action = () => feed.Delete();

        // Assert
        Assert.Throws<DomainException>(action);
    }

}