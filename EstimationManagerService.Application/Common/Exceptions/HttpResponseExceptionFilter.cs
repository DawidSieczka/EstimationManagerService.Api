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
        if (context.Exception is not CustomException exception)
        {
            if (context.Exception is not FluentValidation.ValidationException validationException)
                return;

            var error = validationException.Errors.FirstOrDefault();
            if (error is null)
                return;

            exception = new ValidationException(error.ErrorMessage);
        }

        var errorResponse = _hostEnvironment.IsDevelopment() ?
                new BaseExceptionModel(exception.StatusCode, exception.Message, exception.StackTrace) :
                new BaseExceptionModel(exception.StatusCode, exception.Message);

        context.Result = new JsonResult(exception)
        {
            StatusCode = exception.StatusCode < 600 ? exception.StatusCode : 500,
            Value = errorResponse,
        };
        context.ExceptionHandled = true;
    }
}