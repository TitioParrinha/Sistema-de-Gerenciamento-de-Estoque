using Microsoft.EntityFrameworkCore;
using Estoque.Models;

namespace Estoque.Data
{
    public class EstoqueContext : DbContext
    {
        public DbSet<Gerenciamento> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Criação de banco de dados local
            options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=EstoqueDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}