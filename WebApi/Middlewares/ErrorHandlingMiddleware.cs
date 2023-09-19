
using System.Net;
using System.Text.Json;
using Application.Dto.Common;
using Application.Exceptions;


namespace WebApi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";

                ErrorResponse error = new();

                switch (ex)
                {
                    case CustomValidationException validationEx:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        error.ApplicationErrorMessage = validationEx.ApplicationErrorMessage;
                        error.ErrorMessages = validationEx.ErrorMessages;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        error.ApplicationErrorMessage = ex.Message;
                        break;
                }

                var result = JsonSerializer.Serialize(error);
                await response.WriteAsync(result);
            }
           
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}

