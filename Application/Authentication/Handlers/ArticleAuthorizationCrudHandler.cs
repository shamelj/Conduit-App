﻿using Application.Authentication.Requirements;
using Domain.Features.ArticleFeature;
using Domain.Features.UserFeature.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Application.Authentication.Handlers;

public class ArticleAuthorizationCrudHandler :
    AuthorizationHandler<OperationAuthorizationRequirement, Slug>
{
    private readonly IUserService _userService;

    public ArticleAuthorizationCrudHandler(IUserService userService)
    {
        _userService = userService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement,
        Slug slug)
    {
        var authorizableCrudOperations = new List<string>
        {
            CrudRequirements.Delete.Name,
            CrudRequirements.Update.Name
        };
        var operationNeedsAuthorization = authorizableCrudOperations.Contains(requirement.Name);
        var userHasArticle = await _userService.UserHasArticleAsync(context.User?.Identity?.Name, slug.Value);
        if (userHasArticle && operationNeedsAuthorization)
            context.Succeed(requirement);
    }
}