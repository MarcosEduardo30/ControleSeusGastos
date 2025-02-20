using Microsoft.EntityFrameworkCore;
using Domain.Categorias;
using Domain.Despesas;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Db
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseNpgsql(configuration["postgresCon"]);


        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
    }
}
