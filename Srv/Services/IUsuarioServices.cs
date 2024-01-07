namespace Api.Services;

public interface IUsuarioServices
{
    public Task<Guid> CadastraUsuarioAsync(UsuarioModel usuario);
    public Task<UsuarioModel?> ConsultaPorUUIDAsync(string uuid);
    public Task<IEnumerable<RespostaGetDto>> ConsultaPorTermoAsync(string termo);
    public Task<IEnumerable<UsuarioModel>> RetornaTudoAsync();
    public Task<int> CountUsuariosCadastradosAsync();
}