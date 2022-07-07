using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace EstimationManagerService.Application.Common.Exceptions;

public class HttpResponseExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;

    public HttpResponseExceptionFilter(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not CustomException exception) return;
        var errorResponse = _hostEnvironment.IsDevelopment() ?
            new BaseExceptionModel(exception.StatusCode, exception.Message, exception.StackTrace, exception.InnerException?.Message) :
            new BaseExceptionModel(exception.StatusCode, exception.Message, innerExceptionMessage: exception.InnerException?.Message);

        context.Result = new JsonResult(exception)
        {
            StatusCode = exception.StatusCode < 600 ? exception.StatusCode : 500,
            Value = errorResponse
        };

        context.ExceptionHandled = true;
    }
}