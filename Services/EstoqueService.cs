namespace Estoque.Services;

using Estoque.Models;
using Estoque.Data;
using System.Collections.Generic;
using System.Linq;

public class EstoqueService
{
    private readonly EstoqueContext _db;

    // Receber o banco de dados do Program.cs
    public EstoqueService(EstoqueContext db)
    {
        _db = db;
    }

    // (Create)
    public void AdicionarProduto(Gerenciamento produto)
    {
        _db.Produtos.Add(produto);
        _db.SaveChanges();
    }

    // (Read List)
    public List<Gerenciamento> ListarTodos()
    {
        return _db.Produtos.ToList();
    }

    // (Read ID)
    public Gerenciamento? BuscarPorId(int id)
    {
        return _db.Produtos.Find(id);
    }

    // (Update)
    public bool MexerEstoque(int id, int quantidadeParaRetirar)
    {
        var produto = _db.Produtos.Find(id);

        if (produto == null || produto.Quantidade < quantidadeParaRetirar)
        {
            return false; // Produto não existe ou estoque insuficiente
        }

        produto.Quantidade -= quantidadeParaRetirar;
        _db.SaveChanges();
        return true;
    }

    // (Delete)
    public bool ExcluirProduto(int id)
    {
        var produto = _db.Produtos.Find(id);

        if (produto == null) return false;

        _db.Produtos.Remove(produto);
        _db.SaveChanges();
        return true;
    }
}