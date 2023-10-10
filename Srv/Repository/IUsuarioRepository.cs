namespace Api.Repository;

public interface IUsuarioRepository
{
    public Task GravarUsuarioAsync(UsuarioModel usuario);
    public Task<UsuarioModel?> ConsultarUsuarioPorApelidoAsync(string apelido);
    public Task<List<UsuarioModel>> ConsultarUsuarioPorTermoAsync(string termo);
    public Task<UsuarioModel?> ConsultaUsuarioPorUUIDAsync(string uuid);
    public Task<IEnumerable<UsuarioModel>> RetornaTudoAsync();
    public Task<int> CountUsuariosCadastradosAsync();
}

