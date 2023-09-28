namespace Api.Services;

public interface IUsuarioServices
{
    public Task CadastraUsuarioAsync(DBUsuarioModel usuario);
    public Task<DBUsuarioModel?> ConsultaPorUUIDAsync(string uuid);
    public Task<List<DBUsuarioModel>> ConsultaPorTermoAsync(string termo);
    public Task<List<DBUsuarioModel>> RetornaTudoAsync();
    public Task<int> CountUsuariosCadastradosAsync();
}