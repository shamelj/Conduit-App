using System.Security.Claims;
using Application.Authentication;
using Application.Authentication.Requirements;
using Application.CommentFeature;
using Domain.CommentFeature.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.CommentFeature;
[Route("/api/articles/{slug}/comments")]
[ApiController]
[ConduitExceptionHandlerFilter]
public class CommentController : ControllerBase
{
    private readonly ICommentAppService _commentService;
    private readonly IAuthorizationService _authorizationService;

    public CommentController(ICommentAppService commentService, IAuthorizationService authorizationService)
    {
        _commentService = commentService;
        _authorizationService = authorizationService;
    }
    
    [HttpPost, Authorize]
    public async Task<IActionResult> AddComment([FromRoute] string slug, [FromBody] CommentRequest commentRequest)
    {
        var authenticatedUsername = User.Identity?.Name;
        long id = await _commentService.AddCommentAsync(commentRequest, authenticatedUsername, slug);
        CommentResponse comment = await _commentService.GetCommentAsync(id);
        return Ok(new { Comment = comment });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetComments([FromRoute] string slug)
    {
        var authenticatedUsername = User.Identity?.Name;
        IEnumerable<CommentResponse> comments = await _commentService.GetCommentsAsync(authenticatedUsername, slug);
        return Ok(new { Comments = comments });
    }
    
    [HttpDelete("{id}"), Authorize]
    public async Task<IActionResult> DeleteComment([FromRoute] string slug, [FromRoute] long id) // todo need authorization
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
}