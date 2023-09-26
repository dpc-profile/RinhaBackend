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

            IEnumerable<string>? stack = pessoaDto.Stack;
            if (stack != null)
            {
                foreach (string item in stack)
                {
                    usuario.Stack = usuario.Stack + item + ", ";
                }
            }

            return Created($"/pessoa/{usuario.Id}", pessoaDto);
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