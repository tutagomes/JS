using Microsoft.EntityFrameworkCore;
using Final.Domain.Entities;
using Final.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore.Proxies;

namespace Final.Infra.Data.Context
{
    public class SQLiteContext : DbContext
    {
        public DbSet<Produto> Produto { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Utilizando um servidor SQLite local. Aqui poderíamos configurar qualquer outro banco de dados.
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("DataSource=app.db").UseLazyLoadingProxies(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>(new ProdutoMap().Configure);
        }
    }
}