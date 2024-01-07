namespace Api.Services;

[ExcludeFromCodeCoverage]
public class UsuarioServices : IUsuarioServices
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioServices(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Guid> CadastraUsuarioAsync(UsuarioModel usuario)
    {
        return await _usuarioRepository.GravarUsuarioAsync(usuario);
    }

    public async Task<IEnumerable<RespostaGetDto>> ConsultaPorTermoAsync(string termo)
    {
        List<UsuarioModel> consultaUsuarios = await _usuarioRepository.ConsultarUsuarioPorTermoAsync(termo);

        List<RespostaGetDto> listaUsuarios = new();

        if (consultaUsuarios.Count is 0)
            return listaUsuarios;

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