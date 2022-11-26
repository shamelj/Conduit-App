using System.Text;
using Application.Authentication.Handlers;
using Application.Authentication.Services;
using Application.Features.ArticleFeature.Models;
using Application.Features.ArticleFeature.Services;
using Application.Features.ArticleFeature.Validators;
using Application.Features.CommentFeature.Models;
using Application.Features.CommentFeature.Services;
using Application.Features.CommentFeature.Validtators;
using Application.Features.TagFeature.Services;
using Application.Features.UserFeature.Models;
using Application.Features.UserFeature.Services;
using Application.Features.UserFeature.Validators;
using Domain.Authentication;
using Domain.Features.ArticleFeature.Models;
using Domain.Features.ArticleFeature.Services;
using Domain.Features.CommentFeature.Models;
using Domain.Features.CommentFeature.Services;
using Domain.Features.TagFeature.Services;
using Domain.Features.UserFeature.Services;
using Domain.Shared;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Authentication;
using Infrastructure.Features.ArticleFeature;
using Infrastructure.Features.CommentFeature;
using Infrastructure.Features.TagFeature;
using Infrastructure.Features.UserFeature;
using Infrastructure.Shared;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authentication;

namespace WebAPI.Configurations;

public static class Configuration
{
    public static void AddConduit(this IServiceCollection services, ConfigurationManager configuration)
    {
        AddConduitAuthentication(services, configuration);

        AddConduitDbContext(services, configuration);

        AddRedisConfig(services, configuration);

        AddConduitServicesAndRepositories(services, configuration);

        AddFluentValidation(services);

        AddMapsterConfiguration(services);
    }

    private static void AddConduitAuthentication(IServiceCollection services, ConfigurationManager configuration)
    {
        // authentication scheme
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(configuration.GetSection("Security:Secret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        // authorization handlers
        services.AddScoped<IAuthorizationHandler, ArticleAuthorizationCrudHandler>();
        services.AddScoped<IAuthorizationHandler, CommentAuthorizationCrudHandler>();

        // authentication app service
        services.AddScoped<IAuthenticationAppService>(provider =>
        {
            var userService = provider.GetRequiredService<IUserService>();
            var logoutRepository = provider.GetRequiredService<ILogoutRepository>();
            var secret = configuration.GetSection("Security:Secret").Value;
            var tokenLifetime = Convert.ToDouble(configuration.GetSection("Security:TokenLifetime").Value);
            return new AuthenticationAppService(userService, secret, tokenLifetime, logoutRepository);
        });

        // middleware
        services.AddScoped<LogoutMiddleware>();
    }

    private static void AddConduitServicesAndRepositories(IServiceCollection services,
        ConfigurationManager configuration)
    {
        // domain services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IArticleValidator, ArticleValidator>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // domain repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ILogoutRepository>(provider =>
        {
            var cache = provider.GetRequiredService<IDistributedCache>();
            var tokenLifetime = Convert.ToDouble(configuration.GetSection("Security:TokenLifetime").Value);
            return new RedisLogoutRepository(cache, tokenLifetime);
        });

        // application services
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IArticleAppService, ArticleAppService>();
        services.AddScoped<ICommentAppService, CommentAppService>();
        services.AddScoped<ITagAppService, TagAppService>();
    }

    private static void AddConduitDbContext(IServiceCollection services, ConfigurationManager configuration)
    {
        // DbContext
        services.AddDbContext<ConduitDbContext>(delegate(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConduitDbContext"));
        });
    }

    private static void AddRedisConfig(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDistributedRedisCache(options =>
            options.Configuration = configuration.GetConnectionString("Redis"));
    }

    private static void AddFluentValidation(IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<ArticleRequest>, ArticleRequestValidator>();
        services.AddScoped<IValidator<UserRequest>, UserRequestValidator>();
        services.AddScoped<IValidator<CommentRequest>, CommentRequestValidator>();
    }

    public static void AddMapsterConfiguration(IServiceCollection services)
    {
        TypeAdapterConfig<ArticleEntity, Article>
            .NewConfig()
            .Map(article => article.AuthorUsername, articleEntity => articleEntity.Author.Username);

        TypeAdapterConfig<ArticleEntity, ArticleEntity>
            .NewConfig()
            .Ignore(entity => entity.Tags)
            .Ignore(entity => entity.Id, entity => entity.Id)
            .Ignore(entity => entity.AuthorId)
            .Ignore(entity => entity.Author)
            .IgnoreNullValues(true);

        TypeAdapterConfig<UserEntity, UserEntity>
            .NewConfig()
            .Ignore(entity => entity.Articles)
            .Ignore(entity => entity.Id)
            .Ignore(entity => entity.Comments)
            .Ignore(entity => entity.UserFavouriteArticles)
            .Ignore(entity => entity.UserFollowsUsers)
            .Ignore(entity => entity.UserFollowedByUsers)
            .IgnoreNullValues(true);

        TypeAdapterConfig<CommentEntity, Comment>
            .NewConfig()
            .Map(comment => comment.Username, commentEntity => commentEntity.Author!.Username)
            .Map(comment => comment.ArticleSlug, commentEntity => commentEntity.Article.Slug);
    }
}