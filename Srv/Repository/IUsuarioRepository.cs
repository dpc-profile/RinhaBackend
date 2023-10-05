namespace Api.Repository
{
    public interface IUsuarioRepository
    {
        public Task GravarUsuarioAsync(DBUsuarioModel usuario);
        public Task<DBUsuarioModel> ConsultarUsuarioPorApelidoAsync(string apelido);
        public Task<List<DBUsuarioModel>?> ConsultarUsuarioPorTermoAsync(string termo);
        public Task<DBUsuarioModel?> ConsultaUsuarioPorUUIDAsync(string uuid);
        public Task<IEnumerable<DBUsuarioModel>> RetornaTudoAsync();
        public Task<int> CountUsuariosCadastradosAsync();
    }
}

