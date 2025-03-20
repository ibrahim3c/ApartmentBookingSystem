﻿using System;
using System.Net;
using System.Text.Json;

namespace ApartmentBooking.Api.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context) {
            try
            {
                await _next(context);
            }
            catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = ex switch
            {
                ArgumentNullException => ((int)HttpStatusCode.BadRequest, "A required parameter is missing."),
                ArgumentException => ((int)HttpStatusCode.BadRequest, "Invalid argument provided."),
                KeyNotFoundException => ((int)HttpStatusCode.NotFound, "The requested resource was not found."),
                UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, "Access is denied."),
                InvalidOperationException => ((int)HttpStatusCode.Conflict, "Invalid operation attempted."),
                TimeoutException => ((int)HttpStatusCode.RequestTimeout, "The request timed out."),
                NullReferenceException => ((int)HttpStatusCode.BadRequest, "A null reference occurred."),
                _ => ((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                StatusCode = statusCode,
                Message = message,
                // Detail = ex.Message // Hide this in production for security reasons
            };

            await context.Response.WriteAsJsonAsync(response);
        } 
    }
}
