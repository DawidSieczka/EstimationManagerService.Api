using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;
using EstimationManagerService.Domain.Entities;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<Guid>
{
    public string DisplayName { get; set; }
    public Guid GroupExternalId { get; set; }
}

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly AppDbContext _dbContext;
    private readonly IGuidHelper _guidHelper;

    public CreateProjectCommandHandler(AppDbContext dbContext, IGuidHelper guidHelper)
    {
        _dbContext = dbContext;
        _guidHelper = guidHelper;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var groupEntity = await _dbContext.Groups.FirstOrDefaultAsync(x => x.ExternalId == request.GroupExternalId,
            cancellationToken);

        if (groupEntity is null) throw new NotFoundException("Group", request.GroupExternalId);

        var projectEntityEntry = await _dbContext.Projects.AddAsync(new Project()
        {
            DisplayName = request.DisplayName,
            ExternalId = _guidHelper.CreateGuid(),
            GroupId = groupEntity.Id
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return projectEntityEntry.Entity.ExternalId;
    }
}