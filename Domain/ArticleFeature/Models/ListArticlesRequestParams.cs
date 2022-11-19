using Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace Domain.ArticleFeature.Models;

public class ListArticlesRequestParams
{
    private string? _authorUsername = string.Empty;
    public string? AuthorUsername
    {
        get => _authorUsername;
        set => _authorUsername = value.IsNullOrEmpty()? _authorUsername : value;
    }

    private string? _tagName =string.Empty;
    public string? TagName
    {
        get => _tagName;
        set => _tagName = value.IsNullOrEmpty()? _tagName : value ;
    }

    private string? _favoritedUsername = string.Empty;
    public string? FavoritedUsername
    {
        get => _favoritedUsername;
        set => _favoritedUsername = value.IsNullOrEmpty() ? _favoritedUsername : value;
    }

    
    private string? _feedForUser;

    public string? FeedForUser
    {
        get => _feedForUser;
        set => _feedForUser = value.IsNullOrEmpty() ? _feedForUser : value;
    }

    private int _limit = 20;
    public int Limit
    {
        get => _limit;
        set
        {
            if (value < 0)
                throw new ConduitException() { Message = "Invalid limit value" };
            _limit = value;
        }
    }

    private int _offset = 0;
    public int Offset
    {
        get => _offset;
        set
        {
            if (value < 0)
                throw new ConduitException() { Message = "Invalid offset value" };
            _offset = value;
        }
    }


}