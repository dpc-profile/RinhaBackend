namespace Api.Services;

[ExcludeFromCodeCoverage]
public class UsuarioServices : IUsuarioServices
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioServices(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task CadastraUsuarioAsync(UsuarioModel usuario)
    {
        await _usuarioRepository.GravarUsuarioAsync(usuario);
    }

    public async Task VerificaApelidoCadastradoAsync(string apelido)
    {
        UsuarioModel? consultaUsuarios = await _usuarioRepository.ConsultarUsuarioPorApelidoAsync(apelido);

        if (consultaUsuarios is not null)
            throw new UnprocessableEntityException("Apelido já cadastrado");
    }

    public async Task<IEnumerable<RespostaGetDto>> ConsultaPorTermoAsync(string termo)
    {
        List<UsuarioModel> consultaUsuarios = await _usuarioRepository.ConsultarUsuarioPorTermoAsync(termo);

        if (consultaUsuarios.Count is 0)
            return new List<RespostaGetDto>();

        List<RespostaGetDto> listaUsuarios = new();

        foreach (UsuarioModel usuario in consultaUsuarios)
        {
            listaUsuarios.Add(new RespostaGetDto(){
                Id = usuario.Id,
                Apelido = usuario.Apelido,
                Nome = usuario.Nome,
                Nascimento = usuario.Nascimento,
                Stack = usuario.Stack?.Split(", ").ToList()
            });
        }
        
        return listaUsuarios;
    }

    public async Task<UsuarioModel?> ConsultaPorUUIDAsync(string uuid)
    {
        return await _usuarioRepository.ConsultaUsuarioPorUUIDAsync(uuid);
    }

    public async Task<IEnumerable<UsuarioModel>> RetornaTudoAsync()
    {
        return await _usuarioRepository.RetornaTudoAsync();
    }

    public async Task<int> CountUsuariosCadastradosAsync()
    {
        return await _usuarioRepository.CountUsuariosCadastradosAsync();
    }
}