using Microsoft.EntityFrameworkCore;

namespace Api.Model.Data
{
    public class BancoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string stringconexao;
            string server = Environment.GetEnvironmentVariable("MYSQL_SERVER");
            string user = Environment.GetEnvironmentVariable("MYSQL_USER");
            string pass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

            string db = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            string v = $"server={server};database={db};user={user};password={pass}";
            stringconexao = v;


            optionsBuilder.UseMySql(stringconexao, ServerVersion.AutoDetect(stringconexao));
        }

    }
}