using System;
using EstimationManagerService.Application.Operations.Companies.Commands.CreateCompany;
using EstimationManagerService.Application.Operations.Companies.Commands.DeleteCompany;
using EstimationManagerService.Application.Operations.Companies.Commands.UpdateCompany;
using EstimationManagerService.Application.Operations.Companies.Queries.GetAllUserCompanies;
using EstimationManagerService.Application.Operations.Companies.Queries.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EstimationManagerService.Api.Controllers;

/// <summary>
/// Companies details endpoint.
/// </summary>
public class CompaniesController : ApiController
{
    /// <summary>
    /// Get all companies owned by provided user.
    /// </summary>
    /// <param name="ownerUserId">User external id.</param>
    /// <returns>List of owned companies.</returns>
    [SwaggerResponse(StatusCodes.Status200OK, "Returns all owned companies.", typeof(UserCompanyDto))]
    [HttpGet("{ownerUserId}")]
    public async Task<IActionResult> GetAllUserCompaniesAsync(Guid ownerUserId)
    {
        var companies = await Mediator.Send(new GetAllUserCompaniesQuery() { OnwerUserId = ownerUserId });
        return Ok(companies);
    }

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

    /// <summary>
    /// Deletes user owner's company.
    /// </summary>
    /// <param name="ownerUserId">User external id.</param>
    /// <param name="companyId">Company external id.</param>
    /// <returns>No content.</returns>
    [SwaggerResponse(StatusCodes.Status204NoContent, "Succesfully deleted company.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Owner user hasn't been found.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Company hasn't been found.")]
    [HttpDelete("{ownerUserId}/{companyId}")]
    public async Task<IActionResult> DeleteCompanyAsync(Guid ownerUserId, Guid companyId)
    {
        await Mediator.Send(new DeleteCompanyCommand()
        {
            CompanyExternalId = companyId,
            OwnerUserExternalId = ownerUserId
        });

        return NoContent();
    }
}
