using System.Net;
using System.Text.Json;
using api.Dtos.Shared;
using api.Exceptions;

namespace api.Middlewares
{
    public class ExceptionHandler(RequestDelegate requestHandler)
    {
        private readonly RequestDelegate _RequestHandler = requestHandler;

        public async Task InvokeAsync(HttpContext currentRequest)
        {
            try
            {
                await _RequestHandler(currentRequest);
            }
            catch (Exception exception)
            {
                await HandleException(currentRequest, exception);
            }
        }

        private Task HandleException(HttpContext currentRequest, Exception exception)
        {
            string contentType = "application/json";

            currentRequest.Response.ContentType = contentType;

            ErrorResponse? response;

            if (exception is ModelNotFoundException)
            {
                currentRequest.Response.StatusCode = (int)HttpStatusCode.NotFound;

                response = new(404, exception.Message);

                return currentRequest.Response.WriteAsync(JsonSerializer.Serialize(response));
            }

            if (exception is ConstraintConflictException)
            {
                currentRequest.Response.StatusCode = (int)HttpStatusCode.Conflict;

                response = new(409, exception.Message);

                return currentRequest.Response.WriteAsync(JsonSerializer.Serialize(response));
            }

            currentRequest.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            response = new(500, exception.Message);

            return currentRequest.Response.WriteAsync(JsonSerializer.Serialize(response));

        }
    }
}