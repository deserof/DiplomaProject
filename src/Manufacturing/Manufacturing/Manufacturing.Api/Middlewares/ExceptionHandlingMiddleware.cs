using Manufacturing.Api.ViewModels;

namespace Manufacturing.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                string message = $"Something went wrong: {ex.Message}{Environment.NewLine}{ex.StackTrace}";

                logger.LogError("{message}", message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = GetStatusCode(exception);

            await httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception.Message,
            }
            .ToString());
        }

        private static int GetStatusCode(Exception exception) => exception switch
        {
            ArgumentNullException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
