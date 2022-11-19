using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Filters;

public class ConduitExceptionHandlerFilter : ExceptionFilterAttribute
{
    
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ConduitException exception)
        {
            context.Result = new ObjectResult(new {exception.Message})
            {
                StatusCode = (int?)exception.StatusCode
            };

            context.ExceptionHandled = true;
        }
        else
        {
            base.OnException(context);
        }
    }
}