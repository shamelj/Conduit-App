using Domain.Shared.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Features.ArticleFeature.Models;

public class ListArticlesRequestParams
{
    private string? _authorUsername = string.Empty;

    private string? _favoritedUsername = string.Empty;


    private string? _feedForUser;

    private int _limit = 20;

    private int _offset;

    private string? _tagName = string.Empty;

    public string? AuthorUsername
    {
        get => _authorUsername;
        set => _authorUsername = value.IsNullOrEmpty() ? _authorUsername : value;
    }

    public string? TagName
    {
        get => _tagName;
        set => _tagName = value.IsNullOrEmpty() ? _tagName : value;
    }

    public string? FavoritedUsername
    {
        get => _favoritedUsername;
        set => _favoritedUsername = value.IsNullOrEmpty() ? _favoritedUsername : value;
    }

    public string? FeedForUser
    {
        get => _feedForUser;
        set => _feedForUser = value.IsNullOrEmpty() ? _feedForUser : value;
    }

    public int Limit
    {
        get => _limit;
        set
        {
            if (value < 0)
                throw new ConduitException { Message = "Invalid limit value" };
            _limit = value;
        }
    }

    public int Offset
    {
        get => _offset;
        set
        {
            if (value < 0)
                throw new ConduitException { Message = "Invalid offset value" };
            _offset = value;
        }
    }
}