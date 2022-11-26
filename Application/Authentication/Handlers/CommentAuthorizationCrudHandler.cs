using Application.Authentication.Requirements;
using Domain.Features.CommentFeature;
using Domain.Features.UserFeature.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.Authentication.Handlers;

public class CommentAuthorizationCrudHandler :
    AuthorizationHandler<OperationAuthorizationRequirement, CommentId>
{
    private readonly IUserService _userService;

    public CommentAuthorizationCrudHandler(IUserService userService)
    {
        _userService = userService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        CommentId commentId)
    {
        var authorizableCrudOperations = new List<string>
        {
            CrudRequirements.Delete.Name,
            CrudRequirements.Update.Name
        };
        var operationNeedsAuthorization = authorizableCrudOperations.Contains(requirement.Name);
        var userHasArticle = await _userService.UserHasCommentAsync(context.User?.Identity?.Name, commentId.Value);
        if (userHasArticle && operationNeedsAuthorization)
            context.Succeed(requirement);
    }
}