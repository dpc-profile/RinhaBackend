using Dapper;

using Npgsql;

namespace Api.Repository;
public class UsuarioRepository : IUsuarioRepository
{
    private readonly ILogger<UsuarioRepository> _logger;
    private readonly IConfiguration _config;


    public UsuarioRepository(ILogger<UsuarioRepository> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }
    public async Task<Guid> GravarUsuarioAsync(UsuarioModel usuario)
    {
        string connString = _config.GetConnectionString("postgresql");

        using (var connection = new NpgsqlConnection(connString))
        {
            try
            {
                connection.Open();
                _logger.LogInformation("Conexão aberta com sucesso!");

                string sql = "INSERT INTO pessoas(apelido, nome, nascimento, stack) VALUES(@Apelido, @Nome, @Nascimento, @Stack) ON CONFLICT (apelido) DO NOTHING RETURNING id";
                Guid uuid = await connection.QueryFirstOrDefaultAsync<Guid>(sql, usuario);
                
                return uuid;
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, $"Erro ao abrir a conexão:");
                return Guid.Empty;
            }
            finally
            {
                connection.Close();
                _logger.LogInformation("Conexão fechada.");
            }
        }
    }

    public Task<UsuarioModel?> ConsultarUsuarioPorApelidoAsync(string apelido)
    {
        throw new NotImplementedException();
    }

    public Task<List<UsuarioModel>> ConsultarUsuarioPorTermoAsync(string termo)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioModel?> ConsultaUsuarioPorUUIDAsync(string uuid)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountUsuariosCadastradosAsync()
    {
        throw new NotImplementedException();
    }

    

    public Task<IEnumerable<UsuarioModel>> RetornaTudoAsync()
    {
        throw new NotImplementedException();
    }

}