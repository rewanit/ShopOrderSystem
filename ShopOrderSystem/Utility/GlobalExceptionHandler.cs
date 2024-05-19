using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace ShopOrderSystem.Utility
{
    public class GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
    {
        private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, exception.Message);
                        
            var problemDetails = CreateProblemDetails(context, exception);
            var json = JsonConvert.SerializeObject(problemDetails, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore}) ;

            const string contentType = "application/problem+json";
            context.Response.ContentType = contentType;
            await context.Response.WriteAsync(json, cancellationToken);

            return true;
        }

        private ProblemDetails CreateProblemDetails(in HttpContext context, in Exception exception)
        {
            var statusCode = context.Response.StatusCode;
            var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
            if (string.IsNullOrEmpty(reasonPhrase))
            {
                reasonPhrase = UnhandledExceptionMsg;
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = reasonPhrase,
            };
            problemDetails.Extensions["errorCode"] = exception.HResult;

            if (!env.IsDevelopment())
            {
                return problemDetails;
            }

            problemDetails.Detail = exception.ToString();
            problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            problemDetails.Extensions["data"] = exception.Data;

            return problemDetails;
        }

     
    }
}
