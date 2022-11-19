using System.Security.Claims;
using Application.CommentFeature;
using Domain.CommentFeature.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.CommentFeature;
[Route("/api/articles/{slug}/comments")]
[ApiController]
[ConduitExceptionHandlerFilter]
public class CommentController : ControllerBase
{
    private static readonly string testUsername = "shamel";
    private readonly ICommentAppService _commentService;
    public CommentController(ICommentAppService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddComment([FromRoute] string slug, [FromBody] CommentRequest commentRequest)
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        long id = await _commentService.AddCommentAsync(commentRequest, authenticatedUsername, slug);
        CommentResponse comment = await _commentService.GetCommentAsync(id);
        return Ok(new { Comment = comment });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetComments([FromRoute] string slug)
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        IEnumerable<CommentResponse> comments = await _commentService.GetCommentsAsync(authenticatedUsername, slug);
        return Ok(new { Comments = comments });
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment([FromRoute] string slug, [FromRoute] long id) // todo need authorization
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        await _commentService.DeleteCommentAsync(id);
        return NoContent();
    }
}