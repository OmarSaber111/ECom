using System;
using System.Data;
using System.Net;
using System.Text.Json;
using Ecom.Api.Helper;
using Microsoft.Extensions.Caching.Memory;

namespace Ecom.Api.MiddleWare
{
    //swqwdw
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);

        public ExceptionHandlingMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            _next = next;
            _environment = environment;
            _memoryCache = memoryCache;
        }
        public async Task Invoke(HttpContext context) 
        {
            try
            {
                ApplySecurity(context);
                if (!_environment.IsDevelopment())
                { 

                    if (!IsRequestAllowed(context))
                
                    {
                    context.Response.StatusCode = 429;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                    return;
               
                    }
                }

                await _next(context);
            }
            catch (Exception ex)
            {

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = _environment.IsDevelopment()?
                    new ExceptionHandleResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    :new ExceptionHandleResponse((int)HttpStatusCode.InternalServerError, ex.Message);
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);   

            }
        }
        private bool IsRequestAllowed(HttpContext context) 
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var cashkey = $"Rate:{ip}";
            var datenow = DateTime.Now;
           
            var(timestamp, count) = _memoryCache.GetOrCreate(cashkey, entry =>{
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (datenow, 0);
            });
            if (datenow - timestamp < _rateLimitWindow)
            {
                if (count >= 8)
                {
                    return false;
                }


                _memoryCache.Set(cashkey, (timestamp, count + 1), _rateLimitWindow);
            }
            else
            {
                _memoryCache.Set(cashkey, (datenow, 1), _rateLimitWindow);
            }

            return true;

        }
        private void ApplySecurity(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }
}
