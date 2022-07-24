using EstimationManagerService.Application.Operations.Companies.Commands.DeleteCompany;
using EstimationManagerService.Application.Repositories.Interfaces;
using EstimationManagerService.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EstimationManagerService.IntegrationTests.Tests.Operations.Companies.Commands;

internal class DeleteCompanyTests : TestBase
{
    [Test]
    public async Task DeleteCompany_WithExistingCompany_ShouldRemoveCompany()
    {
        //Arrange
        var user = new User()
        {
            DisplayName = "Admin",
            ExternalId = Guid.NewGuid()
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var company = new Company()
        {
            ExternalId = Guid.NewGuid(),
            DisplayName = "OldCompany",
            AdminId = user.Id
        };

        await _dbContext.Companies.AddAsync(company);
        await _dbContext.SaveChangesAsync();

        //Act
        var usersDbRepositoryMock = new Mock<IUsersDbRepository>();
        usersDbRepositoryMock.Setup(x => x.GetUserIdByUserExternalIdAsync(user.ExternalId, CancellationToken.None)).ReturnsAsync(user.Id);

        var companiesDbRepositoryMock = new Mock<ICompaniesDbRepository>();
        companiesDbRepositoryMock.Setup(x => x.GetOwnersCompany(user.Id, company.ExternalId, CancellationToken.None)).ReturnsAsync(company);

        var command = new DeleteCompanyCommand()
        {
            CompanyExternalId = company.ExternalId,
            OwnerUserExternalId = user.ExternalId
        };

        var handler = new DeleteCompanyCommandHandler(_dbContext, usersDbRepositoryMock.Object, companiesDbRepositoryMock.Object);
        await handler.Handle(command, CancellationToken.None);

        //Assert
        var notExistingCompany = await _dbContext.Companies.FirstOrDefaultAsync(x => x.ExternalId == company.ExternalId);
        notExistingCompany.Should().BeNull();
    }
}