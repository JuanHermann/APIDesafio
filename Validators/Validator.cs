using System.Web.Mvc;

namespace APIDesafio.Validators
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = BadRequestObjectResult(context.ModelState);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}