using Microsoft.EntityFrameworkCore;
using Entidades.Models;

namespace Restful_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; } = null!;
        public DbSet<ContratoCLT> ContratosCLT { get; set; } = null!;
        public DbSet<ContratoPJ> ContratosPJ { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContratoCLT>()
                .HasBaseType(typeof(Contrato));

            modelBuilder.Entity<ContratoPJ>()
                .HasBaseType(typeof(Contrato));
        }
    }
}
