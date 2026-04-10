using Microsoft.EntityFrameworkCore;
using Estoque.Models;

namespace Estoque.Data
{
    public class EstoqueContext : DbContext
    {
        public DbSet<Gerenciamento> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Essa linha cria um banco de dados local no seu PC chamado EstoqueDB
            options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=EstoqueDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}