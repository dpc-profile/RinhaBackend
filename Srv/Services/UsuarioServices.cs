using Microsoft.EntityFrameworkCore;

namespace Api.Services;

class UsuarioServices : IUsuarioServices
{
    private readonly BancoContexto _contexto;

    public UsuarioServices(BancoContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task CadastraUsuarioAsync(DBUsuarioModel usuario)
    {
        await _contexto.Usuarios.AddAsync(usuario);
        await _contexto.SaveChangesAsync();
    }

    public async Task<List<DBUsuarioModel>> ConsultaPorTermoAsync(string termo)
    {
        return await _contexto.Usuarios
            .Where(x => EF.Functions.Like(x.CampoSearch, $"%{termo}%"))
            .ToListAsync();
    }

    public async Task<DBUsuarioModel?> ConsultaPorUUIDAsync(string uuid)
    {
        return await _contexto.Usuarios.FirstOrDefaultAsync(x => x.Id == uuid);
    }

    public async Task<List<DBUsuarioModel>> RetornaTudoAsync()
    {
        return await _contexto.Usuarios.ToListAsync();
    }

    public Task<int> CountUsuariosCadastradosAsync()
    {
        throw new NotImplementedException();
    }
}