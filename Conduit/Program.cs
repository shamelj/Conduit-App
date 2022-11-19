using Application.ArticleFeature;
using Application.CommentFeature;
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
using Microsoft.EntityFrameworkCore;
using WebAPI.Configurations;
using WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelStateFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

app.UseAuthorization();

app.MapControllers();

app.Run();