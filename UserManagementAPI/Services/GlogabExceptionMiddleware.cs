﻿namespace UserManagementAPI.Services
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var errorResponse = new { error = "Internal server error." };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }


}