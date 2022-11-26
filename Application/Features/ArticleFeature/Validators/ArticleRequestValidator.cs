using Application.Features.ArticleFeature.Models;
using FluentValidation;

namespace Application.Features.ArticleFeature.Validators;

public class ArticleRequestValidator : AbstractValidator<ArticleRequest>
{
    public ArticleRequestValidator()
    {
        RuleFor(article => article.Title).NotEmpty();
        RuleFor(article => article.Description).NotEmpty();
        RuleFor(article => article.Body).NotEmpty();
    }
}