using System.ComponentModel.DataAnnotations;

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

            await _usuarioServices.ApelidoCadastradoAsync(pessoaDto.Apelido);

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
        DBUsuarioModel? resposta = await _usuarioServices.ConsultaPorUUIDAsync(uuid);
        return Ok(resposta);
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<string>>> GetPorTermo([Required(ErrorMessage = "O parâmetro 't' é obrigatório.")] string t)
    {
        IEnumerable<RespostaGetDto>? resposta = await _usuarioServices.ConsultaPorTermoAsync(t);
        return Ok(resposta);
    }

    [HttpGet()]
    [Route(template: "/todos-usuarios")]
    public async Task<ActionResult<IEnumerable<string>>> GetAll()
    {
        IEnumerable<DBUsuarioModel>? resposta = await _usuarioServices.RetornaTudoAsync();
        return Ok(resposta);
    }

    [HttpGet]
    [Route("/contagem-pessoas")]
    public async Task<ActionResult<int>> GetAsync()
    {
        return await _usuarioServices.CountUsuariosCadastradosAsync();
    }
}