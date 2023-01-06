using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetProject.Utilities;
using PetProject.Utilities.Exceptions;

namespace PetProject.WebAPI.Filters;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    private readonly ILogger<HttpResponseExceptionFilter> _logger;
    public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger) => _logger = logger;

    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null)
        {
            return;
        }
        ObjectResult objectResult;
        if (context.Exception is PetProjectApplicationException httpResponseException)
        {
            _logger.LogError(context.Exception, PetProjectMessage.TITLE_PROJECT_ERROR);
            objectResult = new ObjectResult(PetProjectMessage.PROJECT_ERROR);
        }
        else if (context.Exception is PetProjectException httpResponsePetProjectException)
        {
            _logger.LogError(context.Exception, PetProjectMessage.TITLE_BUSINESS_ERROR);
            objectResult = new ObjectResult(httpResponsePetProjectException.Message);
        }
        else
        {
#if DEBUG
            objectResult = new ObjectResult(context.Exception);
#else
            _logger.LogError(context.Exception, PetProjectMessage.TITLE_SYSTEM_ERROR);
                objectResult = new ObjectResult(PetProjectMessage.SYSTEM_ERROR);
#endif
        }
        objectResult.StatusCode = StatusCodes.Status500InternalServerError;
        context.ExceptionHandled = true;
        context.Result = objectResult;
    }
}

