namespace Infrastructure.Features.ArticleFeature;

public static class ArticleQueryFilters
{
    public static IQueryable<ArticleEntity> FilterByAuthorUsername(this IQueryable<ArticleEntity> query,
        string? authorUsername)
    {
        return query.Where(article => article.Author.Username.Equals(authorUsername));
    }

    public static IQueryable<ArticleEntity> FilterByFavoritedUsername(this IQueryable<ArticleEntity> query,
        string? favoritedUsername)
    {
        return query.Where(article =>
            article.UserFavouriteArticles
                .Any(userArticle =>
                    userArticle.User.Username.Equals(favoritedUsername)));
    }

    public static IQueryable<ArticleEntity> FilterByTagName(this IQueryable<ArticleEntity> query, string? tagName)
    {
        return query.Where(article =>
            article.Tags.Any(tag =>
                tag.Name.Equals(tagName)));
    }

    public static IQueryable<ArticleEntity> FilterByFollowedAuthors(this IQueryable<ArticleEntity> query,
        string? username)
    {
        return query.Where(article =>
            article.Author.UserFollowedByUsers
                .Any(userFollowedByUser
                    => userFollowedByUser.Follower.Username.Equals(username)));
    }
}