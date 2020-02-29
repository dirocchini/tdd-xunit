using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TDDxUnitCore.Domain._Base;

namespace TDDxUnitCore.Web.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxCall)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = context.Exception is DomainCustomException ? 502 : 500;
                context.Result = context.Exception is DomainCustomException domain
                    ? new JsonResult(domain.ErrorMessages)
                    : new JsonResult("An error occurred");
                context.ExceptionHandled = true;
            }

            base.OnException(context);
        }
    }
}
