using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
                context.HttpContext.Response.StatusCode = 500;
                var message = context.Exception is ArgumentException ? context.Exception.Message : "An error ocurred";
                context.Result = new JsonResult(message);
                context.ExceptionHandled = true;
            }

            base.OnException(context);
        }
    }
}
