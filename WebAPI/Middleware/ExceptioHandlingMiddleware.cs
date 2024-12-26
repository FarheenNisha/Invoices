using Application.Helpers;
using System.Diagnostics;
using System.Net;

namespace WebAPI.Middleware
{
    public class ExceptioHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptioHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
         public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }

            catch (Exception exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var Source = !string.IsNullOrEmpty(exception.Source) ? exception.Source : string.Empty;
                if(env.IsDevelopment())
                {
                    var InnerException = exception.InnerException != null ? exception.InnerException.Message : string.Empty;
                    var StackTrace = !string.IsNullOrEmpty(exception.StackTrace) ? exception.StackTrace.Replace("\r\n", Environment.NewLine).Trim() : string.Empty;
                    var htmlBody = MessageBuilder.BuildExceptionMessage(context, exception);//HTML body can be generated here for sending into email
                    var response = ApiResponseBuilder.GenerateInternalServerError(null, $"{Source}-{exception.Message}", StackTrace);
                    await context.Response.WriteAsJsonAsync(response);
                }
                else
                {
                    var response = ApiResponseBuilder.GenerateInternalServerError(null, $"{Source}-{exception.Message}", null);
                    await context.Response.WriteAsJsonAsync(response);
                }
            }
        }
    }
}
