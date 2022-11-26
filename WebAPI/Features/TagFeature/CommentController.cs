using Application.Features.TagFeature.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Features.TagFeature;

[Route("/api/tags")]
[ApiController]
[ConduitExceptionHandlerFilter]
public class TagController : ControllerBase
{
    private static readonly string TestUsername = "shamel";
    private readonly ITagAppService _tagAppService;

    public TagController(ITagAppService tagAppService)
    {
        _tagAppService = tagAppService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var authenticatedUsername = User.Identity?.Name;
        var tags = await _tagAppService.GetTagsAsync();
        return Ok(new { Tags = tags });
    }
}