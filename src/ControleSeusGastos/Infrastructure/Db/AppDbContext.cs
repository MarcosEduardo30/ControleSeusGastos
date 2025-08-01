using Microsoft.EntityFrameworkCore;
using Domain.Despesas;
using Domain.Usuarios;
using Domain.RefreshToken;

namespace Infrastructure.Db
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("Database"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Despesa>()
                .HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.Usuario_Id)
                .IsRequired();

            modelBuilder.Entity<RefreshToken>()
                .HasKey(rt => rt.Id);

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(rt => rt.Token)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.Usuario)
                .WithMany()
                .HasForeignKey(rt => rt.IdUsuario);
        }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Usuario> Usuarios {  get; set; }
        public DbSet<RefreshToken> RefreshTokens {  get; set; }
    }
}
