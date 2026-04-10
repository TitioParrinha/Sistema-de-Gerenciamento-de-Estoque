using Estoque.Models;
using Estoque.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using var db = new EstoqueContext();
bool sistemaAtivo = true;

Console.WriteLine("--- Sistema de Gerenciamento de Estoque ---");

while (sistemaAtivo)
{
    Console.WriteLine("\nEscolha uma opção:");
    Console.WriteLine("1. Registrar entrada de produto");
    Console.WriteLine("2. Listar produtos");
    Console.WriteLine("3. Sair");
    Console.Write("Opção: ");

    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Gerenciamento novoProduto = new Gerenciamento();
            Console.Write("Nome do produto: ");
            novoProduto.Nome = Console.ReadLine();
            Console.Write("Descrição do produto: ");
            novoProduto.Descricao = Console.ReadLine();
            Console.Write("Quantidade: ");
            if (int.TryParse(Console.ReadLine(), out int qtd))
            {
                novoProduto.Quantidade = qtd;
                
                // SALVANDO NO BANCO
                db.Produtos.Add(novoProduto); // Adiciona na fila
                db.SaveChanges();             // Grava no SQL de verdade!
                
                Console.WriteLine("Produto salvo com sucesso no SQL Server!");
            }
            break;

        case "2":
            Console.WriteLine("\nProdutos em estoque (buscando no banco...):");
            // Buscamos na tabela 'Produtos' do banco
            var listaDeProdutos = db.Produtos.ToList();
            
            foreach (var produto in listaDeProdutos)
            {
                Console.WriteLine($"- ID: {produto.Id} | {produto.Nome}: {produto.Quantidade} unidades");
            }
            break;

        case "3":
            sistemaAtivo = false;
            Console.WriteLine("Encerrando o sistema...");
            break;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            return;
    }
}

