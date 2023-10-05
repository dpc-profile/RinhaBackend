

namespace Api.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly BancoContexto _contexto;

    public UsuarioRepository(BancoContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task GravarUsuarioAsync(DBUsuarioModel usuario)
    {
        await _contexto.Usuarios.AddAsync(usuario);
        await _contexto.SaveChangesAsync();
    }

    public async Task<DBUsuarioModel> ConsultarUsuarioPorApelidoAsync(string apelido)
    {
        return await _contexto.Usuarios.FirstOrDefaultAsync(x => x.Apelido == apelido);
    }

    public async Task<List<DBUsuarioModel>?> ConsultarUsuarioPorTermoAsync(string termo)
    {
        return await _contexto.Usuarios
            .Where(x => EF.Functions.Like(x.CampoSearch, $"%{termo}%"))
            .ToListAsync();
    }

    public async Task<DBUsuarioModel?> ConsultaUsuarioPorUUIDAsync(string uuid)
    {
        return await _contexto.Usuarios.FirstOrDefaultAsync(x => x.Id == uuid);
    }

    public async Task<IEnumerable<DBUsuarioModel>> RetornaTudoAsync()
    {
        return await _contexto.Usuarios.ToListAsync();
    }

    public async Task<int> CountUsuariosCadastradosAsync()
    {
        return await _contexto.Usuarios.CountAsync();
    }
}