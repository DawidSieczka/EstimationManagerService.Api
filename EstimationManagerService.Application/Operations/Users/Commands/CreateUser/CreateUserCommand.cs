using EstimationManagerService.Application.Common.Helpers.MockingHelpers;
using EstimationManagerService.Application.Common.Helpers.MockingHelpers.Interfaces;
using EstimationManagerService.Domain.Entities;
using EstimationManagerService.Persistance;
using MediatR;

namespace EstimationManagerService.Application.Operations.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<Guid>
{
    public string DisplayName { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly AppDbContext _appDbContext;
    private readonly IGuidHelper _guidHelper;

    public CreateUserCommandHandler(AppDbContext appDbContext, IGuidHelper guidHelper)
    {
        _appDbContext = appDbContext;
        _guidHelper = guidHelper;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = new User()
        {
            DisplayName = request.DisplayName,
            ExternalId = _guidHelper.CreateGuid()
        };

        var entityEntry = await _appDbContext.AddAsync(userEntity, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return entityEntry.Entity.ExternalId;
    }
}