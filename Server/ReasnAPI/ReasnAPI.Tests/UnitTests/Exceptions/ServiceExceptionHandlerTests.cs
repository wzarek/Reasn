using System.Net;
using Microsoft.AspNetCore.Http;
using Moq;
using ReasnAPI.Exceptions;
using ReasnAPI.Services.Exceptions;

namespace ReasnAPI.Tests.UnitTests.Exceptions;

[TestClass]
public class ServiceExceptionHandlerTests
{
    private Mock<IProblemDetailsService> _mockProblemDetailsService = null!;
    private ServiceExceptionHandler _handler = null!;
    
    [TestInitialize]
    public void Setup()
    {
        _mockProblemDetailsService = new Mock<IProblemDetailsService>();
        _handler = new ServiceExceptionHandler(_mockProblemDetailsService.Object);
    }
    
    [TestMethod]
    public async Task HandleException_WhenBadRequestException_ShouldReturnProblemDetails()
    {
        var httpContext = new DefaultHttpContext();
        var exception = new BadRequestException("Bad request");

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
        Assert.AreEqual("A bad request was made",
            problemDetailsContext.ProblemDetails.Title);
        Assert.AreEqual(exception, problemDetailsContext.Exception);
        Assert.AreEqual(exception.Message, problemDetailsContext.ProblemDetails.Detail);
    }
    
    [TestMethod]
    public async Task HandleException_WhenNotFoundException_ShouldReturnProblemDetails()
    {
        var httpContext = new DefaultHttpContext();
        var exception = new NotFoundException("Resource not found");

        ProblemDetailsContext? problemDetailsContext = null;
        _mockProblemDetailsService.Setup(x => 
                x.TryWriteAsync(It.IsAny<ProblemDetailsContext>()))
            .Callback<ProblemDetailsContext>(context => problemDetailsContext = context)
            .ReturnsAsync(true);
        
        await _handler.TryHandleAsync(httpContext, exception, CancellationToken.None);
        
        Assert.AreEqual((int)HttpStatusCode.NotFound, httpContext.Response.StatusCode);
        Assert.IsNotNull(problemDetailsContext);
        Assert.AreEqual("https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            problemDetailsContext.ProblemDetails.Type);
        Assert.AreEqual("A resource was not found",
            problemDetailsContext.ProblemDetails.Title);
        Assert.AreEqual(exception, problemDetailsContext.Exception);
        Assert.AreEqual(exception.Message, problemDetailsContext.ProblemDetails.Detail);
    }
    
    [TestMethod]
    public async Task HandleException_WhenVerificationException_ShouldReturnProblemDetails()
    {
        var httpContext = new DefaultHttpContext();
        var exception = new VerificationException("Verification error");

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
        Assert.AreEqual("A verification error occurred",
            problemDetailsContext.ProblemDetails.Title);
        Assert.AreEqual(exception, problemDetailsContext.Exception);
        Assert.AreEqual(exception.Message, problemDetailsContext.ProblemDetails.Detail);
    }
}