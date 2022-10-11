using EstimationManagerService.Application.Common.Exceptions;
using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;
using EstimationManagerService.Domain.Entities;
using EstimationManagerService.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EstimationManagerService.Application.Operations.Groups.Commands.CreateGroup;

public class CreateGroupCommand : IRequest<Guid>
{
    public Guid ExternalCompanyId { get; set; }
    public string DisplayName { get; set; }
}

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
{
    private readonly AppDbContext _dbContext;
    private readonly IGuidHelper _guidHelper;

    public CreateGroupCommandHandler(AppDbContext dbContext, IGuidHelper guidHelper)
    {
        _dbContext = dbContext;
        _guidHelper = guidHelper;
    }

    public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var companyEntity = _dbContext.Companies.FirstOrDefaultAsync(x => x.ExternalId == request.ExternalCompanyId,
            cancellationToken: cancellationToken);

        if (companyEntity is null)
            throw new NotFoundException("Company", request.ExternalCompanyId);

        var groupEntity = await _dbContext.Groups.AddAsync(new Group()
        {
            DisplayName = request.DisplayName,
            CompanyId = companyEntity.Id,
            ExternalId = _guidHelper.CreateGuid()
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return groupEntity.Entity.ExternalId;
    }
}