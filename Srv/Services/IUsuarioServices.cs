namespace Api.Services;

public interface IUsuarioServices
{
    public Task CadastraUsuarioAsync(DBUsuarioModel usuario);
    public Task<DBUsuarioModel?> ConsultaPorUUIDAsync(string uuid);
    public Task ProcurarUsuarioAsync(string apelido);
    public Task<IEnumerable<RespostaGetDto>> ConsultaPorTermoAsync(string termo);
    public Task<IEnumerable<DBUsuarioModel>> RetornaTudoAsync();
    public Task<int> CountUsuariosCadastradosAsync();
}