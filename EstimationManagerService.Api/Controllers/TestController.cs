using Microsoft.AspNetCore.Mvc;

namespace EstimationManagerService.Api.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Nothing to do here. I just init the project with some random things to test if I didn't miss something :P");
        }
    }
}
