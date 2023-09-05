namespace Api.Controllers;

[Route("/pessoas")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    [HttpGet("{uuid}")]
    public ActionResult<string> Get(string uuid)
    {
        return "value";
    }

    [HttpGet()]
    public ActionResult<IEnumerable<string>> GetPorTermo(string t)
    {
        string resultadoDaPesquisa = $"Você pesquisou por: {t}";
        return new string[] { resultadoDaPesquisa, "value2" };
    }
}