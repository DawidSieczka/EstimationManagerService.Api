using EstimationManagerService.Application.Operations.Users.Queries.GetUsers;
using EstimationManagerService.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationManagerService.IntegrationTests.Tests.Operations.Users.Queries;

internal class GetUsersQueryTests : TestBase
{
    [Test]
    public async Task GetUsers_WithExistingCollection_ShouldReturnPaginatedArrayOfUsers()
    {
        //Arrange
        var user1 = new User()
        {
            DisplayName = "user1"
        };

        var user2 = new User()
        {
            DisplayName = "user2"
        };

        var entityEntry1 = await _dbContext.Users.AddAsync(user1);
        var entityEntry2 = await _dbContext.Users.AddAsync(user2);

        await _dbContext.SaveChangesAsync();

        //Act
        var queryResult = await _mediator.Send(new GetUsersQuery() {  Page = 1, PageSize = 10});

        //Assert
        queryResult.Should().NotBeNull();
        queryResult.Data.Should().SatisfyRespectively(first =>
        {
            first.Should().NotBeNull();
            first.ExternalId.Should().Be(entityEntry1.Entity.ExternalId);
            first.DisplayName.Should().Be(entityEntry1.Entity.DisplayName);
        },
        second =>
        {
            second.Should().NotBeNull();
            second.ExternalId.Should().Be(entityEntry2.Entity.ExternalId);
            second.DisplayName.Should().Be(entityEntry2.Entity.DisplayName);
        });
    }

    [Test]
    public async Task GetUsers_WithNotExistingCollection_ShouldReturnEmptyDataCollection()
    {
        //Arrange
        //Act
        var queryResult = await _mediator.Send(new GetUsersQuery() {  Page = 1, PageSize = 10});

        //Assert
        queryResult.Should().NotBeNull();
        queryResult.Data.Should().HaveCount(0);
    }
}
