using Microsoft.AspNetCore.Mvc;

namespace Opinio.API.Controllers;

[ApiController]
public class HealthController : ControllerBase
{
    [HttpGet("api/ping")]
    public IActionResult Ping()
    {
        return Ok(new { message = "Opinio API is running 🚀" });
    }
}