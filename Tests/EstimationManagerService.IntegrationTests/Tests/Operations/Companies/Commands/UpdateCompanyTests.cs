using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;
using EstimationManagerService.Application.Operations.Companies.Commands.UpdateCompany;
using EstimationManagerService.Application.Repositories.Interfaces;
using EstimationManagerService.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EstimationManagerService.IntegrationTests.Tests.Operations.Companies.Commands;

internal class UpdateCompanyTests : TestBase
{
    [Test]
    public async Task UpdateCompany_WithValidInput_ShouldUpdateCompany()
    {
        var user = new User()
        {
            DisplayName = "Admin",
            ExternalId = Guid.NewGuid()
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var company = new Company()
        {
            DisplayName = "Old Name",
            AdminId = user.Id,
            ExternalId = Guid.NewGuid()
        };

        await _dbContext.Companies.AddAsync(company);
        await _dbContext.SaveChangesAsync();

        //Act
        var updateCompanyCommand = new UpdateCompanyCommand()
        {
            DisplayName = "New name",
            OwnerUserExternalId = user.ExternalId,
            CompanyExternalId = company.ExternalId
        };

        var usersDbRepositoryMock = new Mock<IUsersDbRepository>();
        usersDbRepositoryMock.Setup(x=>x.GetUserIdByUserExternalIdAsync(user.ExternalId, CancellationToken.None)).ReturnsAsync(user.Id);
        var companiesDbRepositoryMock = new Mock<ICompaniesDbRepository>();
        companiesDbRepositoryMock.Setup(x => x.GetOwnersCompany(user.Id, company.ExternalId, CancellationToken.None)).ReturnsAsync(company);

        var handler = new UpdateCompanyCommandHandler(_dbContext, usersDbRepositoryMock.Object, companiesDbRepositoryMock.Object);
        await handler.Handle(updateCompanyCommand, CancellationToken.None);
        
        //Assert
        var updatedCompany = await _dbContext.Companies.FirstOrDefaultAsync(x=>x.Id == company.Id);
        updatedCompany.Should().NotBeNull();
        updatedCompany?.ExternalId.Should().Be(company.ExternalId);
        updatedCompany?.AdminId.Should().Be(company.AdminId);
        updatedCompany?.DisplayName.Should().Be(updateCompanyCommand.DisplayName);
    }
}
