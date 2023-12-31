namespace Api.Model.Data;

public class BancoContexto : DbContext
{
    public BancoContexto(DbContextOptions<BancoContexto> options) : base(options)
    {
    }

    public DbSet<UsuarioModel> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Defina aqui as configurações de mapeamento de suas entidades
        modelBuilder.Entity<UsuarioModel>().ToTable("Usuarios");
    }
}
