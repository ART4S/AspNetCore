using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using WebFeatures.Application.Infrastructure.Exceptions;

namespace WebFeatures.WebApi.Filters
{
    /// <summary>
    /// Обработчик исключений
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationEx)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                context.Result = new JsonResult(validationEx.Fail);

                return;
            }

            context.HttpContext.Response.ContentType = "text/plain";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Result = new ObjectResult("Неизвестная ошибка сервера");
        }
    }
}
