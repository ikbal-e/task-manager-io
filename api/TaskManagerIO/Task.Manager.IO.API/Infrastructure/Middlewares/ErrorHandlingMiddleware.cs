using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace TaskManagerIO.API.Infrastructure.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exeption)
        {
            await HandleExceptions(context, exeption);
        }
    }

    private static Task HandleExceptions(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = 500;

        // TODO: log

        var problem = new ProblemDetails
        {
            Title = "Unhandled error occured",
            Detail = "Contact with traceId",
            Status = 500,
            Instance = context.Request.Path,
        };

        problem.Extensions.Add("traceId", Activity.Current?.Id ?? context.TraceIdentifier);
        // TODO: research traceId

        var result = JsonSerializer.Serialize(problem);

        return context.Response.WriteAsync(result);
    }
}