using NUnit.Framework;
using System.Threading.Tasks;

namespace EstimationManagerService.IntegrationTests;

using static SetupTests;

public class TestBase
{
    [SetUp]
    public async Task SetUp()
    {
        await ResetState();
    }
}