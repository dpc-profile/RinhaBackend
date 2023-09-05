namespace Api.Repository;

public interface IUsuarioRepository
{
    public Task<UsuarioModel> BuscarPorId(Guid uuid);
    public Task<UsuarioModel> BuscarPorTermo(string termo);
    public Task<string> ContaUsuarios();
}