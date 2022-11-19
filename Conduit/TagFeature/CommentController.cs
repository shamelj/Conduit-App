using System.Security.Claims;
using Application.CommentFeature;
using Application.TagFeature;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.TagFeature;
[Route("/api/tags")]
[ApiController]
[ConduitExceptionHandlerFilter]
public class TagController : ControllerBase
{
    private static readonly string testUsername = "shamel";
    private readonly ITagAppService _tagAppService;
    public TagController(ITagAppService tagAppService)
    {
        _tagAppService = tagAppService;
    }
  
    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var authenticatedUsername = User.FindFirstValue("Username") ?? testUsername;
        IEnumerable<string> tags = await _tagAppService.GetTagsAsync();
        return Ok(new { Tags = tags });
    }
}