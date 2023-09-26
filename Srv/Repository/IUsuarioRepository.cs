namespace Api.Repository;

public interface IUsuarioRepository
{
    public Task<DBUsuarioModel> BuscarPorId(Guid uuid);
    public Task<DBUsuarioModel> BuscarPorTermo(string termo);
    public Task<string> ContaUsuarios();
}