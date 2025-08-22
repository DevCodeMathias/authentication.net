using API_AUTENTICATION.domain.exception;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace API_AUTENTICATION.infrastructure.middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task  InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EmailAlreadyExistsException ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status409Conflict);
            }
            catch (BusinessException ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context,ex, StatusCodes.Status500InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new { message = ex.Message };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
