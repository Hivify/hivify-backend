
using BuildingBlocks.ApplicationPorts.Messaging;
using Feeds.Application.Commands.CreateFeed;
using Feeds.Application.Commands.UpdateFeed;
using Feeds.Application.Queries.GetFeeds;
using Feeds.Application.Queries.GetSingleFeed;
using Hivify.Api.Controllers.Feeds.Requests;
using Hivify.Api.Controllers.Feeds.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Hivify.Api.Controllers.Feeds;

[ApiController]
[Route("api/feeds")]
public sealed class FeedsController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IQuerySender _querySender;

    public FeedsController(
        ISender sender,
        IQuerySender querySender)
    {
        _sender = sender;
        _querySender = querySender;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GetFeedRes>>> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(new GetFeedsQuery(), cancellationToken);

        var response = result
            .Select(feed => new GetFeedRes(
                feed.Id,
                feed.Title,
                feed.Content,
                feed.CreatedDate))
            .ToList();

        return Ok(response);
    }

    [HttpGet("{feedId:guid}")]
    public async Task<ActionResult<GetFeedRes>> GetById(
        Guid feedId,
        CancellationToken cancellationToken)
    {
        var result = await _querySender.Send(
            new GetSingleFeedQuery(feedId),
            cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        var response = new GetFeedRes(
            result.Id,
            result.Title,
            result.Content,
            result.CreatedDate);

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<GetFeedRes>> Create(
        CreateFeedRequest request,
        CancellationToken cancellationToken)
    {
        var feedId = await _sender.Send(
            new CreateFeedCommand(
                request.Title,
                request.Description),
            cancellationToken);

        var result = await _querySender.Send(
            new GetSingleFeedQuery(feedId),
            cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        var response = new GetFeedRes(
            result.Id,
            result.Title,
            result.Content,
            result.CreatedDate);

        return CreatedAtAction(
            nameof(GetById),
            new { feedId },
            response);
    }

    [HttpPut("{feedId:guid}")]
    public async Task<IActionResult> Update(
       Guid feedId,
       UpdateFeedRequest request,
       CancellationToken cancellationToken)
    {
        await _sender.Send(
            new UpdateFeedCommand(
                feedId,
                request.Title,
                request.Description),
            cancellationToken);

        return NoContent();
    }
}

