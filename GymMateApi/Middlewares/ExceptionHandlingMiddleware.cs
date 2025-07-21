using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace GymMateApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                var (statusCode, message) = ex switch
                {
                    EntityNotFoundException => (HttpStatusCode.NotFound, ex.Message),
                    BadRequestException => (HttpStatusCode.BadRequest, ex.Message),
                    _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
                };

                await HandleExceptionAsync(context, ex.Message, statusCode, message);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            string exceptionMessage,
            HttpStatusCode httpStatusCode,
            string message)
        {
            _logger.LogError(exceptionMessage);

            HttpResponse response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            ErrorDto errorDto = new()
            {
                Message = message,
                StatusCode = (int)httpStatusCode
            };

            await response.WriteAsJsonAsync(errorDto);
        }
    }
}
