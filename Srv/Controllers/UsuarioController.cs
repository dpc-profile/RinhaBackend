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
    public ActionResult Post([FromBody] PessoaDto pessoaDto)
    {
        try
        {
            pessoaDto.Validate();

            DBUsuarioModel usuario = new()
            {
                Id = Guid.NewGuid(),
                Nome = pessoaDto.Nome,
                Nascimento = pessoaDto.Nascimento,
                Apelido = pessoaDto.Apelido
            };

            if (pessoaDto.Stack != null)
                usuario.Stack = string.Join( ", ", pessoaDto.Stack);

            usuario.CampoSearch += $"{usuario.Nome},{usuario.Apelido},{usuario.Stack}";

            return Created($"/pessoa/{usuario.Id}", usuario);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (UnprocessableEntityException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    [HttpGet("{uuid}")]
    public ActionResult<string> Get(string uuid)
    {
        return Ok("value1");
    }

    [HttpGet()]
    public ActionResult<IEnumerable<string>> GetPorTermo(string t)
    {
        string resultadoDaPesquisa = $"Você pesquisou por: {t}";
        return Ok(new string[] { resultadoDaPesquisa, "value2" });
    }
}