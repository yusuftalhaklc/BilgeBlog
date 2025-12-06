using BilgeBlog.Application.DTOs.Common;
using BilgeBlog.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace BilgeBlog.WebApi.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string message;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = notFoundException.Message;
                    break;

                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = badRequestException.Message;
                    break;

                case UnauthorizedException unauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = unauthorizedException.Message;
                    break;

                case ForbiddenException forbiddenException:
                    statusCode = HttpStatusCode.Forbidden;
                    message = forbiddenException.Message;
                    break;

                case ConflictException conflictException:
                    statusCode = HttpStatusCode.Conflict;
                    message = conflictException.Message;
                    break;

                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = validationException.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An error occurred while processing your request.";
                    break;
            }

            var response = BaseResponse<object>.Fail(message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}

