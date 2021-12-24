using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BooshiWebApi.Filters
{
    public class MyActionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine(context.Exception.GetType());
            var httpContext = context.HttpContext;
            if (context.Exception is SecurityTokenExpiredException)
            {
                httpContext.Response.StatusCode = 203;
                httpContext.Response.Cookies.Delete("jwt");
                httpContext.Response.WriteAsJsonAsync(new {Message = "Token expired"});
                context.ExceptionHandled = true;
                return;
            }
            if(context.Exception is ArgumentNullException && context.Exception.Source == "System.IdentityModel.Tokens.Jwt")
            {
                httpContext.Response.StatusCode = 401;
                context.ExceptionHandled = true;
                return;
            }
            if (context.Exception is Exception)
            {
                context.HttpContext.Response.StatusCode = 400;
                context.ExceptionHandled = true;
                return;
            }
        }
    }
}
