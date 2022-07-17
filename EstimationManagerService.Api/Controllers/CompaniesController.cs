using System;
using EstimationManagerService.Application.Operations.Companies.Commands.CreateCompany;
using EstimationManagerService.Application.Operations.Companies.Commands.UpdateCompany;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstimationManagerService.Api.Controllers;

/// <summary>
/// Companies details endpoint.
/// </summary>
public class CompaniesController : ApiController
{

    /// <summary>
    /// Create company as its Admin.
    /// </summary>
    /// <param name="command">Create company model.</param>
    /// <returns>Company external id.</returns>
    [SwaggerResponse(StatusCodes.Status201Created, "Returns created company id.", typeof(Guid))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Owner user id hasn't been found.")]
    [HttpPost]
    public async Task<IActionResult> CreateCompanyAsync(CreateCompanyCommand command)
    {
        var companyExternalId = await Mediator.Send(command);

        var createdRoute = GetCreatedRoute(nameof(CompaniesController), companyExternalId);
        return Created(createdRoute, companyExternalId);
    }


    /// <summary>
    /// Update company as its Admin.
    /// </summary>
    /// <param name="command">Update company model.</param>
    /// <returns>No Content.</returns>
    [SwaggerResponse(StatusCodes.Status204NoContent, "Succesfully updated company.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Owner user hasn't been found.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Company hasn't been found.")]
    [HttpPut]
    public async Task<IActionResult> UpdateCompanyAsync(UpdateCompanyCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}
