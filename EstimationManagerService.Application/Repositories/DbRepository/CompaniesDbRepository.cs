using System;
using EstimationManagerService.Persistance;

namespace EstimationManagerService.Application.Repositories.DbRepository;

public class CompaniesDbRepository
{
    private readonly AppDbContext _dbContext;

    public CompaniesDbRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

}
