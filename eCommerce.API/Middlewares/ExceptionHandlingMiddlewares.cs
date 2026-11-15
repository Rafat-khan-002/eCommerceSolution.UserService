using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace eCommerce.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddlewares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddlewares> _loger;

        public ExceptionHandlingMiddlewares(RequestDelegate next,ILogger<ExceptionHandlingMiddlewares> logger)
        {
            _next = next;
            _loger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // log the exception type and message
                _loger.LogError($"{ex.GetType().ToString()}:{ex.Message}");

               if(ex.InnerException is not null)
                {
                    //inner exception type and  message
                    _loger.LogError($"{ex.InnerException.ToString()}:{ex.InnerException.Message}");
                }
                //inter server error
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(new { Message = ex.Message, type = ex.GetType().ToString() });
            }
           
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewaresExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddlewares(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddlewares>();
        }
    }
}
