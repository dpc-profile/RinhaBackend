
namespace Api.Controllers;

[Route("/pessoas")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioServices _usuarioServices;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioServices usuarioServices)
    {
        _logger = logger;
        _usuarioServices = usuarioServices;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] PessoaDto pessoaDto)
    {
        try
        {
            pessoaDto.Validate();

            DBUsuarioModel usuario = new()
            {
                Id = Guid.NewGuid().ToString(),
                Nome = pessoaDto.Nome,
                Nascimento = pessoaDto.Nascimento,
                Apelido = pessoaDto.Apelido
            };

            if (pessoaDto.Stack != null)
                usuario.Stack = string.Join( ", ", pessoaDto.Stack);

            usuario.CampoSearch += $"{usuario.Nome},{usuario.Apelido},{usuario.Stack}";

            await _usuarioServices.CadastraUsuarioAsync(usuario);

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
    public async Task<ActionResult<string>> GetPorUuidAsync(string uuid)
    {
        var resposta = await _usuarioServices.ConsultaPorUUIDAsync(uuid);
        return Ok(resposta);
    }

    [HttpGet()]
    public ActionResult<IEnumerable<string>> GetPorTermo(string t)
    {
        var resposta = _usuarioServices.ConsultaPorTermoAsync(t);
        return Ok(resposta);
    }
}