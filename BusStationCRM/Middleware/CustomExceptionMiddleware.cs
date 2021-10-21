using System;
using System.Net;
using System.Threading.Tasks;
using BusStationCRM.Exceptions;
using BusStationCRM.Models;
using Microsoft.AspNetCore.Http;

namespace BusStationCRM.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleHttpExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleHttpExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            context.Response.ContentType = "application/json";

            var result = new ErrorDetails()
            {
                Message = exception.Message,
                StatusCode = (int)exception.StatusCode
            }.ToString();
            context.Response.StatusCode = (int)exception.StatusCode;

            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = new ErrorDetails()
            {
                Message = exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError
            }.ToString();
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
    }
}
