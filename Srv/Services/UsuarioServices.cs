namespace Api.Services;

public class UsuarioServices : IUsuarioServices
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioServices(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task CadastraUsuarioAsync(DBUsuarioModel usuario)
    {
        await _usuarioRepository.GravarUsuarioAsync(usuario);
    }

    public async Task VerificaApelidoCadastradoAsync(string apelido)
    {
        DBUsuarioModel? resultado = await _usuarioRepository.ConsultarUsuarioPorApelidoAsync(apelido);

        if (resultado is not null)
            throw new UnprocessableEntityException("Apelido já cadastrado");
    }

    public async Task<IEnumerable<RespostaGetDto>> ConsultaPorTermoAsync(string termo)
    {
        List<DBUsuarioModel>? resposta = await _usuarioRepository.ConsultarUsuarioPorTermoAsync(termo);

        if (resposta.Count is 0)
            return new List<RespostaGetDto>();

        List<RespostaGetDto> listaUsuarios = new();

        foreach (DBUsuarioModel usuario in resposta)
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

    public async Task<DBUsuarioModel?> ConsultaPorUUIDAsync(string uuid)
    {
        return await _usuarioRepository.ConsultaUsuarioPorUUIDAsync(uuid);
    }

    public async Task<IEnumerable<DBUsuarioModel>> RetornaTudoAsync()
    {
        return await _usuarioRepository.RetornaTudoAsync();
    }

    public async Task<int> CountUsuariosCadastradosAsync()
    {
        return await _usuarioRepository.CountUsuariosCadastradosAsync();
    }
}