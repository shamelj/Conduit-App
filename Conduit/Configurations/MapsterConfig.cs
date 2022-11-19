using Domain.ArticleFeature.Models;
using Domain.CommentFeature.Models;
using Infrastructure.ArticleFeature;
using Infrastructure.CommentFeature;
using Mapster;

namespace WebAPI.Configurations;

public static class MapsterConfig
{
    public static void AddMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<ArticleEntity, Article>
            .NewConfig()
            .Map(article => article.AuthorUsername, articleEntity => articleEntity.Author.Username);
        
        TypeAdapterConfig<ArticleEntity, ArticleEntity>
            .NewConfig()
            .Ignore(entity => entity.Tags)
            .Ignore(entity => entity.Id,entity => entity.Id )
            .Ignore(entity => entity.AuthorId)
            .Ignore(entity => entity.Author)
            .IgnoreNullValues(true);

        TypeAdapterConfig<CommentEntity, Comment>
            .NewConfig()
            .Map(comment => comment.Username, commentEntity => commentEntity.Author!.Username)
            .Map(comment => comment.ArticleSlug, commentEntity => commentEntity.Article.Slug);
    }
    
}