using Announcement_Domain.Exeptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Task_Service.Filters
{
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(new { errorMessage = context.Exception.Message });
            }
            else if (context.Exception is ValidationException)
            {
                context.Result = new BadRequestObjectResult(new { errorMessage = context.Exception.Message });
            }
            else if(context.Exception is AlreadyExistException)
            {
                context.Result = new BadRequestObjectResult(new { errorMessage = context.Exception.Message });
            }
        }
    }
}
