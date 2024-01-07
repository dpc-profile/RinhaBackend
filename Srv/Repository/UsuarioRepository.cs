using Dapper;

using Npgsql;

namespace Api.Repository;
public class UsuarioRepository : IUsuarioRepository
{
    private readonly ILogger<UsuarioRepository> _logger;
    private readonly IConfiguration _config;
    private readonly string? _coonString;


    public UsuarioRepository(ILogger<UsuarioRepository> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
        _coonString = _config.GetConnectionString("postgresql");;
    }
    public async Task<Guid?> GravarUsuarioAsync(UsuarioModel usuario)
    {
        using (var connection = new NpgsqlConnection(_coonString))
        {
            try
            {
                connection.Open();
                _logger.LogInformation("Conexão aberta com sucesso!");

                string sql = "SELECT INS_PESSOA(@pApelido, @pNome, @pNascimento, @pStack)";

                var parametros = new
                {
                    pApelido = usuario.Apelido,
                    pNome = usuario.Nome,
                    pNascimento = usuario.Nascimento,
                    pStack = usuario.Stack
                };

                Guid? uuid = await connection.QueryFirstOrDefaultAsync<Guid?>(sql, parametros, commandType: System.Data.CommandType.Text);

                return uuid;
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, $"Erro ao abrir a conexão:");
                return null;
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