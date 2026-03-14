using Microsoft.AspNetCore.Mvc;

namespace PokemonStrategyBuilder.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Status = "OK",
            Service = "Pokemon Strategy Builder API"
        });
    }
}
