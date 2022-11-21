using System.Text;
using Application.ArticleFeature;
using Application.Authentication.Handlers;
using Application.Authentication.Services;
using Application.CommentFeature;
using Application.TagFeature;
using Application.UserFeature;
using Domain.ArticleFeature.Services;
using Domain.CommentFeature.Services;
using Domain.Shared;
using Domain.TagFeature.Services;
using Domain.UserFeature.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.ArticleFeature;
using Infrastructure.CommentFeature;
using Infrastructure.Shared;
using Infrastructure.TagFeature;
using Infrastructure.UserFeature;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Authentication;
using WebAPI.Configurations;
using WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers(options => { options.Filters.Add<ModelStateFilter>(); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("Security:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Authorization handlers
builder.Services.AddScoped<IAuthorizationHandler, ArticleAuthorizationCrudHandler>();
builder.Services.AddScoped<IAuthorizationHandler, CommentAuthorizationCrudHandler>();



// DbContext
builder.Services.AddDbContext<ConduitDbContext>(delegate(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("ConduitDbContext"));
});
// domain services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// domain repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
// application services
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<IArticleAppService, ArticleAppService>();
builder.Services.AddScoped<ICommentAppService, CommentAppService>();
builder.Services.AddScoped<ITagAppService, TagAppService>();
builder.Services.AddScoped<IAuthenticationAppService>(provider =>
{
    var userService = provider.GetRequiredService<IUserService>();
    var secret = builder.Configuration.GetSection("Security:Secret").Value;
    var tokenLifetime = Convert.ToDouble(builder.Configuration.GetSection("Security:TokenLifetime").Value);
    return new AuthenticationAppService(userService, secret, tokenLifetime);
});


//validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<ArticleRequest>, ArticleRequestValidator>();

// mapster config
builder.Services.AddMapsterConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();