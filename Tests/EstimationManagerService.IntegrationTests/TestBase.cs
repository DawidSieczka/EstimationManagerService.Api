﻿using EstimationManagerService.Persistance;
using MediatR;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EstimationManagerService.IntegrationTests;

using static SetupTests;

public class TestBase 
{
    protected AppDbContext _dbContext { get; set; }
    protected ISender _mediator { get; set; }
    [SetUp]
    public async Task SetUp()
    {
        await ResetState();
        _dbContext = GetDbContext();
        _mediator = GetMediator();
    }
}