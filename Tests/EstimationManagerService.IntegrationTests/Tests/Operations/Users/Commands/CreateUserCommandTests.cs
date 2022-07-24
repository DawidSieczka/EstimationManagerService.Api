using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;
using EstimationManagerService.Application.Operations.Users.Commands.CreateUser;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EstimationManagerService.IntegrationTests.Tests.Operations.Users.Commands;

public class CreateUserCommandTests : TestBase
{
    [Test]
    public async Task CreateUser_WithValidInput_ShouldReturnCreatedUserExternalId()
    {
        //Arrange
        var expectedGuidValue = Guid.NewGuid();

        var guidHelperMock = new Mock<IGuidHelper>();
        guidHelperMock.Setup(x => x.CreateGuid()).Returns(expectedGuidValue);

        //Act
        var createUserCommand = new CreateUserCommand()
        {
            DisplayName = "New User"
        };

        var command = new CreateUserCommandHandler(_dbContext, guidHelperMock.Object);
        var resultExternalId = await command.Handle(createUserCommand, CancellationToken.None);

        //Assert
        resultExternalId.Should().Be(expectedGuidValue);

        var resultUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.ExternalId == expectedGuidValue);
        resultUser.Should().NotBeNull();
        resultUser?.DisplayName.Should().Be(createUserCommand.DisplayName);
    }

    [Test]
    public async Task CreateUser_WithInvalidInput_ShouldNotCreateUser()
    {
        //Arrange
        var createUserCommand = new CreateUserCommand();

        //Act
        //var result = await _mediator.Send(createUserCommand, CancellationToken.None);
        var result = await FluentActions.Invoking(() => _mediator.Send(createUserCommand)).Should().ThrowAsync<DbUpdateException>();
        
        //Assert
        var existingUsersCount = await _dbContext.Users.CountAsync();
        existingUsersCount.Should().Be(0);
    }
}