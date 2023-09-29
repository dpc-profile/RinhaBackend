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

    public async Task VerificaApelidoCadastradoAsync(string apelido)
    {
        DBUsuarioModel? resultado = await _contexto.Usuarios.FirstOrDefaultAsync(x => x.Apelido == apelido);

        if (resultado is not null)
            throw new UnprocessableEntityException("Apelido já cadastrado");
    }

    public async Task<IEnumerable<RespostaGetDto>> ConsultaPorTermoAsync(string termo)
    {
        var resposta = await _contexto.Usuarios
            .Where(x => EF.Functions.Like(x.CampoSearch, $"%{termo}%"))
            .ToListAsync();

        if (resposta.Count is 0)
            return new List<RespostaGetDto>();

        List<RespostaGetDto> listaUsuarios = new();

        foreach (DBUsuarioModel usuario in resposta)
        {
            listaUsuarios.Add(new RespostaGetDto(){
                Id = usuario.Id,
                Apelido = usuario.Apelido,
                Nome = usuario.Nome,
                Nascimento = usuario.Nascimento,
                Stack = usuario.Stack?.Split(", ").ToList()
            });
        }
        
        return listaUsuarios;
    }

    public async Task<DBUsuarioModel?> ConsultaPorUUIDAsync(string uuid)
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