using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Support.Validation
{
    public class ValidateModelStateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.SelectMany(s => s.Value.Errors.Select(e => e.ErrorMessage)).ToList();
                (context.Controller as Controller).TempData["Error"] = errors;
                context.Result = new RedirectToRouteResult(context.RouteData.Values);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
