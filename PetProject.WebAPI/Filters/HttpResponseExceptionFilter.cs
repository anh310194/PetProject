using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetProject.Core.Exceptions;

namespace PetProject.WebAPI.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            ObjectResult objectResult;
            if (context.Exception is PetProjectException httpResponseException)
            {
                objectResult = new ObjectResult(httpResponseException.Message);
            }
            else
            {
#if DEBUG
                objectResult = new ObjectResult(context.Exception);
#else                
objectResult = new ObjectResult("The system error. Please contact Addmin!");
#endif
            }
            objectResult.StatusCode = StatusCodes.Status500InternalServerError;
            context.ExceptionHandled = true;
            context.Result = objectResult;
        }
    }
}
