using GymMateApi.Application.Dto;
using GymMateApi.Application.Exceptions;
using System.Net;

namespace GymMateApi.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var (statusCode, message) = ex switch
                {
                    EntityNotFoundException => (HttpStatusCode.NotFound, ex.Message),
                    BadRequestException => (HttpStatusCode.BadRequest, ex.Message),
                    UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ex.Message),
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
            logger.LogError(exceptionMessage);

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
