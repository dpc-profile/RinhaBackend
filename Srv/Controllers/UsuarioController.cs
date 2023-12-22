namespace Api.Controllers;

[Route("/pessoas")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioServices _usuarioServices;

    public UsuarioController(IUsuarioServices usuarioServices)
    {
        _usuarioServices = usuarioServices;
    }

    [HttpPost]
    public async Task<IActionResult> GravaUsuarioAsync([FromBody] PessoaDto pessoaDto)
    {
        try
        {
            pessoaDto.Validate();

            await _usuarioServices.VerificaApelidoCadastradoAsync(pessoaDto.Apelido);

            UsuarioModel usuario = pessoaDto.PopulaUsuarioModel();

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
        UsuarioModel? resposta = await _usuarioServices.ConsultaPorUUIDAsync(uuid);
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
        IEnumerable<UsuarioModel>? resposta = await _usuarioServices.RetornaTudoAsync();
        return Ok(resposta);
    }

    [HttpGet]
    [Route("/contagem-pessoas")]
    public async Task<ActionResult<int>> CountAsync()
    {
        return await _usuarioServices.CountUsuariosCadastradosAsync();
    }
}