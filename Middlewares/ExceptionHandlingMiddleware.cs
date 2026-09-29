using DeskFlow.Api.Models.DTOs.Erros;

namespace DeskFlow.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var errorResponse = new ErrorResponseDTO("Ocorreu um erro não catalogado, tente novamente ou entre em contato com o suporte técnico.");
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}