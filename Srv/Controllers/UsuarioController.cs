namespace Api.Controllers;

[Route("/pessoas")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioServices _usuarioServices;
    private readonly ILogger<UsuarioRepository> _logger;


    public UsuarioController(IUsuarioServices usuarioServices, ILogger<UsuarioRepository> logger)
    {
        _usuarioServices = usuarioServices;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> GravaUsuarioAsync([FromBody] PessoaDto pessoaDto)
    {
        try
        {
            pessoaDto.Validate();

            // await _usuarioServices.VerificaApelidoCadastradoAsync(pessoaDto.Apelido);

            UsuarioModel usuario = pessoaDto.PopulaUsuarioModel();

            Guid uuid = await _usuarioServices.CadastraUsuarioAsync(usuario);

            if (uuid == Guid.Empty)
                throw new UnprocessableEntityException("Apelido já cadastrado");

            return Created($"/pessoa/{uuid}", pessoaDto);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (UnprocessableEntityException e)
        {
            return UnprocessableEntity(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError("Exceção não esperada.", e);
            return BadRequest(e.Message);
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