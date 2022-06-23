using Microsoft.EntityFrameworkCore;
using Entidades.Models;

namespace Restful_API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<FuncionarioCLT> FuncionariosCLT { get; set; }
        public DbSet<FuncionarioPJ> FuncionariosPJ { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuncionarioCLT>().HasBaseType(typeof(Funcionario));
            modelBuilder.Entity<FuncionarioPJ>().HasBaseType(typeof(Funcionario));
        }
    }
}
