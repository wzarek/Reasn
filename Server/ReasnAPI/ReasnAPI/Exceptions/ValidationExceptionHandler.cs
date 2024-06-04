using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace ReasnAPI.Exceptions;

public class ValidationExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public ValidationExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        var errors = validationException.Errors.Select(e => new
        {
            field = e.PropertyName,
            message = e.ErrorMessage
        }).ToList();

        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = "A validation error occurred",
                Detail = "One or more validation errors occurred",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Extensions =
                {
                    ["errors"] = errors
                }
            },
            Exception = validationException
        });
    }
}