using Application.Authentication.Requirements;
using Application.Features.CommentFeature.Models;
using Application.Features.CommentFeature.Services;
using Domain.Features.CommentFeature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Features.CommentFeature;

[Route("/api/articles/{slug}/comments")]
[ApiController]
[ConduitExceptionHandlerFilter]
public class CommentController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ICommentAppService _commentService;

    public CommentController(ICommentAppService commentService, IAuthorizationService authorizationService)
    {
        _commentService = commentService;
        _authorizationService = authorizationService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddComment([FromRoute] string slug, [FromBody] HttpCommentRequest commentRequest)
    {
        var authenticatedUsername = User.Identity?.Name;
        var id = await _commentService.AddCommentAsync(commentRequest.Comment, authenticatedUsername, slug);
        var comment = await _commentService.GetCommentAsync(id, authenticatedUsername);
        return Ok(new { Comment = comment });
    }

    [HttpGet]
    public async Task<IActionResult> GetComments([FromRoute] string slug)
    {
        var authenticatedUsername = User.Identity?.Name;
        var comments = await _commentService.GetCommentsAsync(authenticatedUsername, slug);
        return Ok(new { Comments = comments });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment([FromRoute] string slug, [FromRoute] long id)
    {
        var authorizationResult =
            await _authorizationService.AuthorizeAsync(User, new CommentId(id), CrudRequirements.Delete);
        if (authorizationResult.Succeeded)
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }

        return Forbid();
    }

    public class HttpCommentRequest
    {
        public CommentRequest Comment { get; set; }
    }
}