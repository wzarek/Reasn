using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Moq;
using ReasnAPI.Exceptions;

namespace ReasnAPI.Tests.UnitTests.Exceptions;

[TestClass]
public class ValidationExceptionHandlerTests
{
    private Mock<IProblemDetailsService> _mockProblemDetailsService = null!;
    private ValidationExceptionHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockProblemDetailsService = new Mock<IProblemDetailsService>();
        _handler = new ValidationExceptionHandler(_mockProblemDetailsService.Object);
    }

    [TestMethod]
    public async Task HandleException_WhenValidationException_ShouldReturnProblemDetails()
    {
        var httpContext = new DefaultHttpContext();
        var exception = new ValidationException(new List<ValidationFailure>
        {
            new ("Email", "'Email' must not be empty."),
            new ("Password", "'Password' must not be empty.")
        });

        ProblemDetailsContext? problemDetailsContext = null;
        _mockProblemDetailsService.Setup(x =>
                x.TryWriteAsync(It.IsAny<ProblemDetailsContext>()))
            .Callback<ProblemDetailsContext>(context => problemDetailsContext = context)
            .ReturnsAsync(true);

        await _handler.TryHandleAsync(httpContext, exception, CancellationToken.None);

        Assert.AreEqual((int)HttpStatusCode.BadRequest, httpContext.Response.StatusCode);
        Assert.IsNotNull(problemDetailsContext);
        Assert.AreEqual("https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            problemDetailsContext.ProblemDetails.Type);
        Assert.AreEqual("A validation error occurred",
            problemDetailsContext.ProblemDetails.Title);
        Assert.AreEqual(exception, problemDetailsContext.Exception);
        Assert.AreEqual("One or more validation errors occurred",
            problemDetailsContext.ProblemDetails.Detail);

        Assert.IsNotNull(problemDetailsContext.ProblemDetails.Extensions);
        Assert.IsTrue(problemDetailsContext.ProblemDetails.Extensions.ContainsKey("errors"));
    }
}