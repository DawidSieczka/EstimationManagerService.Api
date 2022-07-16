using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstimationManagerService.Api.Controllers;

/// <summary>
/// Base Api controller that accepts basic configuration for all controllers.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    /// <summary>
    /// Encapsulated mediator instance handling existing ISender instance.
    /// </summary>
    private ISender _mediator;

    /// <summary>
    /// Shared ISender instance of mediator for all controllers.
    /// </summary>
    /// <typeparam name="ISender"></typeparam>
    /// <returns></returns>
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}