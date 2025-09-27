using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BookGallery.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status;
            string title;

            switch (ex)
            {
                case UnauthorizedAccessException:
                    status = HttpStatusCode.Unauthorized;
                    title = "Unauthorized";
                    break;

                case KeyNotFoundException:
                    status = HttpStatusCode.NotFound;
                    title = "Resource not found";
                    break;

                case ArgumentException or InvalidOperationException:
                    status = HttpStatusCode.BadRequest;
                    title = "Invalid request";
                    break;

                case ApplicationException:
                    status = HttpStatusCode.Forbidden;
                    title = "Forbidden";
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    title = "Unexpected error occurred";
                    break;
            }

            context.Response.StatusCode = (int)status;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = (int)status,
                Title = title,
                Detail = _env.IsDevelopment() ? ex.ToString() : ex.Message,
                Instance = context.Request.Path
            };

            await context.Response.WriteAsJsonAsync(problem);
        }

    }
}