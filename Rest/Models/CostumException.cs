﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rest.Models
{
    public class CostumException : ExceptionFilterAttribute, IExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            //Business exception-More generics for external world
            var error = new ErrorDetails()
            {
                StatusCode = 500,
                Message = "Something went wrong! Internal Server Error."
            };
            //Logs your technical exception with stack trace below

            context.Result = new JsonResult(error);
            return Task.CompletedTask;
        }
    }
}
