using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;
using EstimationManagerService.Application.Operations.Companies.Commands.CreateCompany;
using EstimationManagerService.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EstimationManagerService.IntegrationTests.Tests.Operations.Companies.Commands;

internal class CreateCompanyTests : TestBase
{
    [Test]
    public async Task CreateCompany_WithValidInput_ShoulCreateCompany()
    {
        //Arrange
        var user = new User()
        {
            DisplayName = "Admin",
            ExternalId = Guid.NewGuid()
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var expectedGuid = Guid.NewGuid();

        //Act
        var createCompanyCommand = new CreateCompanyCommand()
        {
            DisplayName = "AnyName",
            OwnerUserExternalId = user.ExternalId
        };

        var guidHelperMock = new Mock<IGuidHelper>();
        guidHelperMock.Setup(x => x.CreateGuid()).Returns(expectedGuid);

        var handler = new CreateCompanyCommandHandler(_dbContext, guidHelperMock.Object);
        var result = await handler.Handle(createCompanyCommand, CancellationToken.None);

        //Assert
        result.Should().Be(expectedGuid);

        var createdCompany = await _dbContext.Companies.FirstOrDefaultAsync(x => x.ExternalId == expectedGuid);
        createdCompany.Should().NotBeNull();
        createdCompany?.ExternalId.Should().Be(expectedGuid);
        createdCompany?.DisplayName.Should().Be(createCompanyCommand.DisplayName);
        createdCompany?.AdminId.Should().Be(user.Id);
        createdCompany?.Groups.Should().BeNull();
    }

    [Test]
    public async Task CreateCompany_WithInvalidInput_ShouldNotCreateCompany()
    {
        //Arrange
        var createCompanyCommand = new CreateCompanyCommand();

        //Act
        //var result = await _mediator.Send(createUserCommand, CancellationToken.None);
        var result = await FluentActions.Invoking(() => _mediator.Send(createCompanyCommand)).Should().ThrowAsync<NotFoundException>();

        //Assert
        var existingCompaniesCount = await _dbContext.Companies.CountAsync();
        existingCompaniesCount.Should().Be(0);
    }
}