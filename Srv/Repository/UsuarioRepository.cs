namespace Api.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly BancoContexto _contexto;

    public UsuarioRepository(BancoContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task GravarUsuarioAsync(UsuarioModel usuario)
    {
        await _contexto.Usuarios.AddAsync(usuario);
        await _contexto.SaveChangesAsync();
    }

    public async Task<UsuarioModel?> ConsultarUsuarioPorApelidoAsync(string apelido)
    {
        UsuarioModel? dBUsuarioModel = await _contexto.Usuarios.FirstOrDefaultAsync(x => x.Apelido == apelido);
        return dBUsuarioModel;
    }

    public async Task<List<UsuarioModel>> ConsultarUsuarioPorTermoAsync(string termo)
    {
        return await _contexto.Usuarios
            .Where(x => EF.Functions.Like(x.CampoSearch, $"%{termo}%"))
            .ToListAsync();
    }

    public async Task<UsuarioModel?> ConsultaUsuarioPorUUIDAsync(string uuid)
    {
        return await _contexto.Usuarios.FirstOrDefaultAsync(x => x.Id == uuid);
    }

    public async Task<IEnumerable<UsuarioModel>> RetornaTudoAsync()
    {
        return await _contexto.Usuarios.ToListAsync();
    }

    public async Task<int> CountUsuariosCadastradosAsync()
    {
        return await _contexto.Usuarios.CountAsync();
    }
}