using Estoque.Models;
using Estoque.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

using var db = new EstoqueContext();
bool sistemaAtivo = true;

Console.WriteLine("--- Sistema de Gerenciamento de Estoque ---");

while (sistemaAtivo)
{
    Console.WriteLine("\nEscolha uma opção:");
    Console.WriteLine("1. Registrar entrada de produto");
    Console.WriteLine("2. Listar produtos");
    Console.WriteLine("3. Editar Produto");
    Console.WriteLine("4. Descrição do produto");
    Console.WriteLine("5. Deletar Produto");
    Console.WriteLine("6. Sair");
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
            Console.Write("Digite a identificação(ID) do produto: ");

            if(int.TryParse(Console.ReadLine(), out int IdParaSalvar))
            {

            var Produto = db.Produtos.Find(IdParaSalvar);

                if (Produto != null)
                {
                    Console.WriteLine($"(Produto Encontrado: {Produto.Nome})  (Quantidade: {Produto.Quantidade})");
                    Console.Write("Quantidade para retirar: ");

                    if(int.TryParse(Console.ReadLine(), out int qtdSaida) && qtdSaida > 0 )
                    {

                            if(Produto.Quantidade >= qtdSaida)
                        {
                            Produto.Quantidade -= qtdSaida;
                            db.SaveChanges();
                            Console.WriteLine("Retirada salva com sucesso!");
                        }

                            else
                        {
                            Console.WriteLine("Falha na retirada do produto (Estoque Insuficiente)");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Quantidade Inválida");
                    }
                }
                else
                {
                    Console.WriteLine($"Produto com identificação {0} não encontrado.", IdParaSalvar);
                }
            }
            break;

        case "4":
            Console.Write("Digite a Identificação(ID) do produto: ");

            if(int.TryParse(Console.ReadLine(), out int IdParaDescricao))
            {
                var Produto = db.Produtos.Find(IdParaDescricao);
                
                Console.WriteLine($"-----{Produto.Nome}-----");
                Console.WriteLine($"{Produto.Descricao}");

            }
            break;

        case "5":

            Console.Write("Digite a Identificação(ID) do produto que deseja remover: ");

            if(int.TryParse(Console.ReadLine(), out int IdParaExcluir))
            {
                var Produto = db.Produtos.Find(IdParaExcluir);
                
                if(Produto != null)
                {
                    Console.WriteLine($"Tem certeza que Deseja Excluir '{Produto.Nome}'? (S/N)");
                    string confirmacao = Console.ReadLine()?.ToUpper() ?? "N";

                    if(confirmacao == "S")
                    {
                        db.Produtos.Remove(Produto);

                        db.SaveChanges();

                        Console.Write("Exclusão de produto concluída.");
                    }
                    else
                    {
                        Console.WriteLine("Exclusão cancelada.");
                    }
                }
                else
                {
                    Console.Write("Produto não Encontrado.");
                }

            }
            break;

        case "6":
            sistemaAtivo = false;
            Console.WriteLine("Encerrando o sistema...");
            break;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            return;
    }
}

