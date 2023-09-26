namespace Api.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    public Task<DBUsuarioModel> BuscarPorId(Guid uuid)
    {
        throw new NotImplementedException();
    }

    public Task<DBUsuarioModel> BuscarPorTermo(string termo)
    {
        throw new NotImplementedException();
    }

    public Task<string> ContaUsuarios()
    {
        throw new NotImplementedException();
    }
}