namespace Api.Services;

public interface IUsuarioServices
{
    public Task CadastraUsuario(DBUsuarioModel usuario);
    public Task ConsultaPorUUID(string uuid);
    public Task ConsultaPorTermo(string termo);
}