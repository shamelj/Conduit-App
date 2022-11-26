using Application.Features.CommentFeature.Models;
using FluentValidation;

namespace Application.Features.CommentFeature.Validtators;

public class CommentRequestValidator : AbstractValidator<CommentRequest>
{
    public CommentRequestValidator()
    {
        RuleFor(comment => comment.Body).NotEmpty();
    }
}