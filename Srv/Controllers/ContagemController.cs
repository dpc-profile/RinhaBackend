namespace Api.Controllers;

[Route("/contagem-pessoas")]
[ApiController]
public class ContagemController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        return new string[] { "value1", "value2" };
    }
}