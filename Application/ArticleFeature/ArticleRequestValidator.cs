using FluentValidation;

namespace Application.ArticleFeature;

public class ArticleRequestValidator : AbstractValidator<ArticleRequest>
{
    public ArticleRequestValidator()
    {
        RuleFor(article => article.Title).NotEmpty();
        RuleFor(article => article.Description).NotEmpty();
        RuleFor(article => article.Body).NotEmpty();
    }
}