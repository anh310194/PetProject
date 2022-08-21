using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetProject.Core.Exceptions;

namespace PetProject.WebAPI.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        private readonly ILogger<HttpResponseExceptionFilter> _logger;
        public HttpResponseExceptionFilter(ILogger<HttpResponseExceptionFilter> logger) => _logger = logger;

        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ExceptionHandled)
            {
                return;
            }
            ObjectResult objectResult;
            if (context.Exception is PetProjectException httpResponseException)
            {
                _logger.LogError(context.Exception, "Bussiness Error");
                objectResult = new ObjectResult(httpResponseException.Message);
            }
            else
            {
#if DEBUG
                objectResult = new ObjectResult(context.Exception);
#else   
                _logger.LogError(context.Exception, "System Error");
                objectResult = new ObjectResult("The system error. Please contact Addmin!");
#endif
            }
            objectResult.StatusCode = StatusCodes.Status500InternalServerError;
            context.ExceptionHandled = true;
            context.Result = objectResult;
        }
    }
}
