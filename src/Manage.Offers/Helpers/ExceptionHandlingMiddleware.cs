using Manage.Offers.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Manage.Offers.Helpers
{

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var baseException = exception.GetBaseException();

            var result = JsonConvert.SerializeObject(
                new
                {
                    baseException.Message
                });

            if (!context.Response.HasStarted)
                context.Response.OnStarting(
                      async () =>
                      {
                          context.Response.ContentType = "application/json";
                          context.Response.StatusCode = (int)await MapExceptionToStatusCode(exception);
                      });

            return context.Response.WriteAsync(result);
        }

        private static ValueTask<HttpStatusCode> MapExceptionToStatusCode(Exception exception)
        {
            return exception.GetType().Name switch
            {
                nameof(BrokerNotFoundException) => new ValueTask<HttpStatusCode>(HttpStatusCode.NotFound),
                nameof(OfferNotFoundException) => new ValueTask<HttpStatusCode>(HttpStatusCode.NotFound),
                nameof(ForeignKeyException) => new ValueTask<HttpStatusCode>(HttpStatusCode.BadRequest),
                _ => new ValueTask<HttpStatusCode>(HttpStatusCode.InternalServerError)
            };
        }
    }
}
