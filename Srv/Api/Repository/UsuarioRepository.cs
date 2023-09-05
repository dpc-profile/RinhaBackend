namespace Api.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    public Task<UsuarioModel> BuscarPorId(Guid uuid)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioModel> BuscarPorTermo(string termo)
    {
        throw new NotImplementedException();
    }

    public Task<string> ContaUsuarios()
    {
        throw new NotImplementedException();
    }
}