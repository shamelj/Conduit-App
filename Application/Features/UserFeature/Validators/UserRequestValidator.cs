using Application.Features.UserFeature.Models;
using FluentValidation;

namespace Application.Features.UserFeature.Validators;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(user => user.Username).NotEmpty();
        RuleFor(user => user.Email).EmailAddress();
        RuleFor(user => user.Password).MinimumLength(8);
    }
}