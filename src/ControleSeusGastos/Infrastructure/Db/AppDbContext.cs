using Microsoft.EntityFrameworkCore;
using Domain.Categorias;
using Domain.Despesas;
using Microsoft.Extensions.Configuration;
using Domain.Usuarios;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Despesa>()
                .HasOne(e => e.Categoria)
                .WithMany()
                .HasForeignKey(e => e.Categoria_Id);

            modelBuilder.Entity<Despesa>()
                .HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.Usuario_Id)
                .IsRequired();
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Usuario> Usuarios {  get; set; }
    }
}
