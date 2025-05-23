using Microsoft.AspNetCore.Mvc;

namespace WorkshopManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("WorkshopManager API is running");
        }
    }
}