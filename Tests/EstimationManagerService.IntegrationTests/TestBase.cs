using EstimationManagerService.Persistance;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EstimationManagerService.IntegrationTests;

using static SetupTests;

public class TestBase 
{
    protected AppDbContext _dbContext { get; set; }

    [SetUp]
    public async Task SetUp()
    {
        await ResetState();
        _dbContext = GetDbContext();
    }
}