using EstimationManagerService.Application.Services.Interfaces;
using EstimationManagerService.Domain.Entities;
using EstimationManagerService.Infrastructure;
using MediatR;

namespace EstimationManagerService.Application.Operations.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public string DisplayName { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly AppDbContext _appDbContext;
    private readonly IGuidService _guidService;

    public CreateUserCommandHandler(AppDbContext appDbContext, IGuidService guidService)
    {
        _appDbContext = appDbContext;
        _guidService = guidService;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = new User()
        {
            DisplayName = request.DisplayName,
            ExternalId = _guidService.CreateGuid()
        };

        var entityEntry = await _appDbContext.AddAsync(userEntity, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return entityEntry.Entity.Id;
    }
}