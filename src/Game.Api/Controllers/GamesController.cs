namespace Game.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {

        return Ok();
    }

    [HttpPost]
    public IActionResult Create()
    {

        return Ok();
    }
}