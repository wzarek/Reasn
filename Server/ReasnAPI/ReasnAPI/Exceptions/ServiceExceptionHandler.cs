using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ReasnAPI.Exceptions;

public class ServiceExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public ServiceExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Detail = exception.Message
        };

        switch (exception)
        {
            case BadRequestException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                problemDetails.Title = "A bad request was made";
                break;

            case NotFoundException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
                problemDetails.Title = "A resource was not found";
                break;

            case VerificationException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                problemDetails.Title = "A verification error occurred";
                break;

            case UnauthorizedAccessException:
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
                problemDetails.Title = "Unauthorized";
                break;

            default:
                return false;
        }

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
            Exception = exception
        });
    }
}