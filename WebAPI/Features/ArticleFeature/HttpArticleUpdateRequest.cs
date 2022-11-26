using Application.Features.ArticleFeature.Models;

namespace WebAPI.Features.ArticleFeature;

public class HttpArticleUpdateRequest
{
    public ArticleUpdateRequest Article { get; set; }
}