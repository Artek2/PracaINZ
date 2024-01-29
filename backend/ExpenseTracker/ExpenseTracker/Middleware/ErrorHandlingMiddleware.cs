// Namespace declaration for the middleware
namespace ExpenseTracker.Middleware
{
    // Custom middleware class for global error handling in the application.
    public class ErrorHandlingMiddleware : IMiddleware
    {
        // Logger service provided by Microsoft.Extensions.Logging
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        // Constructor with dependency injection for the logger.
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger; // Assigning the injected logger to the local field.
        }

        // Asynchronous method representing the middleware's logic.
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                // Calling the next middleware in the pipeline.
                await next.Invoke(context);
            }
            catch (Exception e)
            {
                // Logging the error using Microsoft's logging framework.
                _logger.LogError(e, e.Message);

                // Setting the HTTP response status code to 500 - Internal Server Error.
                context.Response.StatusCode = 500;
                // Writing the exception message to the HTTP response.
                // Uses Microsoft.AspNetCore.Http for context.Response functionality.
                await context.Response.WriteAsync(e.Message);
            }
        }
    }
}
