
using Loja.Busca;
using Loja.DataBase.Context;
using Loja.DataBase.Repositories;
using Loja.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Loja.Menus;

public class MenuProduto : MenuPrincipal
{
    LojaContext context = new LojaContext();
    public void Menu()
    {
        int opcao = -4;
        while (opcao != 1 && opcao != 2 && opcao != 3 && opcao != 4 && opcao != 5)
        {
            try
            {
                base.ExibirTexto("Menu produtos");
                Console.WriteLine("\n1 - [Cadastrar]");
                Console.WriteLine("2 - [Remover]");
                Console.WriteLine("3 - [Listar]");
                Console.WriteLine("4 - [Atualizar]");
                Console.WriteLine("5 - [Sair]");
                Console.WriteLine("\nEscolha uma das opções válidas:");
                opcao = int.Parse(Console.ReadLine()!);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Opção inválida!");
                base.PressioneEProssiga();
            }
        }
        switch (opcao)
        {
            case 1:
                Cadastro(context);
                base.PressioneEProssiga();

                Menu();
                break;

            case 2:
                Remover(context);
                base.PressioneEProssiga();
                Menu();
                break;
            case 3:
                Listar(context);
                base.PressioneEProssiga();
                Menu();
                break;
            case 4:
                Atualizar(context);
                base.PressioneEProssiga();
                Menu();
                break;

            default:

                break;
        }

    }
    public void Cadastro(LojaContext context)
    {
        base.LimparTela();
        RepositorioGenerico<Produto> repositorioProduto = new(context);
        Buscas busca = new Buscas(context);
        base.ExibirTexto("Registrar produto");
        Console.WriteLine("Informe o nome do produto que deseja cadastrar:");
        string nomeProduto = Console.ReadLine()!;
        var consultarProduto = busca.BuscaProduto(nomeProduto);
        double preco = 0;
        if (consultarProduto is null)
        {
            try
            {
                base.LimparTela();
                Console.WriteLine("Informe o preço do produto:");
                preco = double.Parse(Console.ReadLine()!);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Entrada inválida!");
                Thread.Sleep(1000);
                base.LimparTela();

            }
            Console.WriteLine($"Infome o fornecedor de {nomeProduto}:");
            string nomeFornecedor = Console.ReadLine()!;
            var consultarFornecedor = busca.BuscaFornecedor(nomeFornecedor);
            if (consultarFornecedor is null)
            {
                base.LimparTela();
                Console.WriteLine("Fornecedor não encontrado!");

                string resp = "";
                while (resp != "S" && resp != "N")
                {
                    Console.WriteLine($"Deseja cadastrar {nomeFornecedor} como novo fornecedor?[S/N]");
                    resp = Console.ReadLine()!.ToUpper();
                }
                if (resp == "S")
                {
                    base.LimparTela();
                    RepositorioGenerico<Fornecedor> repositorioFornecedor = new(context);
                    Fornecedor fornecedor = new Fornecedor();
                    fornecedor.Nome = nomeFornecedor;
                    repositorioFornecedor.Adicionar(fornecedor);
                    Produto novoProduto = new Produto() { Nome = nomeProduto, Preco = preco };
                    novoProduto.Fornecedor = fornecedor;
                    repositorioProduto.Adicionar(novoProduto);
                    base.LimparTela();
                    Console.WriteLine("Cadastro realizado com sucesso!");
                }
                else
                {
                    base.LimparTela();
                    Console.WriteLine("Cancelando cadastro.\nVoltando ao menu...");

                }
            }
            else
            {
                base.LimparTela();
                Produto novoProduto = new Produto() { Nome = nomeProduto, Preco = preco };
                novoProduto.Fornecedor = consultarFornecedor;
                repositorioProduto.Adicionar(novoProduto);
                base.LimparTela();
                Console.WriteLine("Cadastro realizado com sucesso!");
            }
        }
        else
        {
            base.LimparTela();
            Console.WriteLine("Esse nome já está cadastrado!");
            Thread.Sleep(1500);
        }
    }
    public void Remover(LojaContext context)
    {
        base.LimparTela();
        RepositorioGenerico<Produto> repositorioProduto = new(context);
        Buscas busca = new Buscas(context);
        base.ExibirTexto("Remover produto");
        Console.WriteLine("Informe o nome do produto que deseja excluir:");
        string nomeProduto = Console.ReadLine()!;
        var consultaProduto = busca.BuscaProduto(nomeProduto);
        if (consultaProduto is not null)
        {
            base.LimparTela();
            string resp = "";
            while (resp != "S" && resp != "N")
            {
                Console.WriteLine($"Tem certeza que deseja excluir permanentemente o produto {consultaProduto.Nome}? [S/N]");
                resp = Console.ReadLine()!.ToUpper();
            }
            if (resp == "S")
            {
                base.LimparTela();
                Console.WriteLine("Excluindo dados...");
                repositorioProduto.Excluir(consultaProduto);
            }
            else
            {
                base.LimparTela();
                Console.WriteLine("Tudo bem!\nVoltando ao menu...");
            }
        }
        else
        {
            base.LimparTela();
            Console.WriteLine($"O produto {nomeProduto} não foi encontrado no banco de dados!");
        }
    }
    public void Listar(LojaContext context)
    {
        RepositorioGenerico<Produto> repositorioProduto = new(context);
        Buscas busca = new Buscas(context);
        base.LimparTela();
        base.ExibirTexto("Lista de produtos");
        int opcao = 0;
        while (opcao != 1 && opcao != 2 && opcao != 3)
        {
            try
            {
                Console.WriteLine("1 - [Listagem de A - Z]");
                Console.WriteLine("2 - [Lista de produto por fornecedor]");
                Console.WriteLine("3 - [Lista de produto por maior valor]");
                opcao = int.Parse(Console.ReadLine()!);
            }
            catch (Exception e)
            {
                Console.WriteLine("Opção inválid!");
                base.PressioneEProssiga();
            }
        }
        switch (opcao)
        {
            case 1:
                base.LimparTela();
                base.ExibirTexto("Lista de produtos A - Z");
                var listaProdutosAZ = repositorioProduto.Listar().OrderBy(p => p.Nome);
                foreach (var produto in listaProdutosAZ)
                {
                    Console.WriteLine(produto.Nome);
                }
                break;
            case 2:
                base.LimparTela();
                Console.WriteLine("Informe o nome do fornecedor que deseja buscar:");
                string fornecedor = Console.ReadLine()!;
                var consultarFornecedor = busca.BuscaFornecedor(fornecedor);
                if (consultarFornecedor is null)
                {
                    base.LimparTela();
                    Console.WriteLine("O fornecedor informado não existe!");
                }
                else
                {
                    base.LimparTela();
                    var listaProdutosFornecedor = repositorioProduto.ListarPor(p => p.Fornecedor!.Nome.Equals(consultarFornecedor.Nome));
                    if (listaProdutosFornecedor is null || !listaProdutosFornecedor.Any())
                    {
                        base.LimparTela();
                        Console.WriteLine("O fornecedor ainda não possui produtos cadastrados!");
                    }
                    else
                    {
                        base.ExibirTexto($"Produtos do forncedor {consultarFornecedor.Nome}");

                        foreach (var produto in listaProdutosFornecedor)
                        {
                            Console.WriteLine(produto.Nome);
                        }
                    }
                }
                break;

            case 3:
                base.LimparTela();
                var listaProdutosPorMaiorValor = repositorioProduto.Listar().OrderByDescending(p => p.Preco);
                if (listaProdutosPorMaiorValor is null || !listaProdutosPorMaiorValor.Any())
                {
                    base.LimparTela();
                    Console.WriteLine("A lista de produtos se encotra vazia no momento!");
                }
                else
                {
                    base.LimparTela();
                    base.ExibirTexto("Lista de produtos por maior preço");
                    Console.WriteLine();
                    foreach (var produto in listaProdutosPorMaiorValor)
                    {
                        Console.WriteLine($" Produto: {produto.Nome}, Valor: R$ {produto.Preco}");
                    }
                }
                break;
            default:

                break;
        }
    }
    public void Atualizar(LojaContext context)
    {
        base.LimparTela();
        RepositorioGenerico<Produto> repositorioProduto = new(context);
        Buscas busca = new(context);

        Console.WriteLine("Informe o nome do produto que deseja atualizar:");
        string nomeProduto = Console.ReadLine()!;
        Console.Clear();
        var consultarProduto = busca.BuscaProduto(nomeProduto);

        if (consultarProduto is null)
        {
            base.LimparTela();
            Console.WriteLine("Produto não encontrado!");
        }
        else
        {
            int opcao = 0;
            while (opcao != 1 && opcao != 2 && opcao != 3)
            {
                try
                {
                    Console.WriteLine("[1 - Alterar fornecedor]");
                    Console.WriteLine("[2 - Altear nome]");
                    Console.WriteLine("[3 - Alterar preço]");
                    opcao = int.Parse(Console.ReadLine()!);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Entrada inválida!");

                }
            }
            switch (opcao)
            {
                case 1:
                    base.LimparTela();
                    Console.WriteLine($"Informe o nome do novo fornecedor para {consultarProduto.Nome}:");
                    string novoFornecedor = Console.ReadLine()!;
                    var consultarFornecedor = busca.BuscaFornecedor(novoFornecedor);
                    if (consultarFornecedor is null)
                    {
                        base.LimparTela();
                        Console.WriteLine("Fornecedor não existe no sistema!");
                    }
                    else
                    {
                        if(consultarFornecedor.Id == consultarProduto.FornecedorId)
                        {
                            base.LimparTela();
                            Console.WriteLine("Este fornecedor já pertence a esse cliente!");
                        }
                        else
                        {
                            base.LimparTela();
                            consultarProduto.Fornecedor = consultarFornecedor;
                            repositorioProduto.Atualizar(consultarProduto);
                            Console.WriteLine($"Atualização do produto {consultarProduto.Nome} foi realizada!");
                        }
                    }
                    break;
                case 2:
                    base.LimparTela();
                    Console.WriteLine("Informe o novo nome para o produto digitado:");
                    string novoNomeProduto = Console.ReadLine()!;
                    var consultarExistenciaProduto = busca.BuscaProduto(novoNomeProduto);
                    if (consultarExistenciaProduto is not null)
                    {
                        base.LimparTela();
                        Console.WriteLine("Já existe produto com esse nome!");
                    }
                    else
                    {
                        base.LimparTela();
                        Console.WriteLine($"O nome do produto {consultarProduto.Nome} passou a ser {novoNomeProduto}");
                        consultarProduto.Nome = novoNomeProduto;
                        repositorioProduto.Atualizar(consultarProduto);
                    }

                    break;
                case 3:
                    base.LimparTela();
                    try
                    {
                        Console.WriteLine("Informe o novo preço do produto:");
                        double novoPreco = double.Parse(Console.ReadLine()!);
                        consultarProduto.Preco = novoPreco;
                        repositorioProduto.Atualizar(consultarProduto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("entrada inválida!");
                    }

                    break;
                default:
                    Console.WriteLine("Opção indisponível!");
                    break;
            }

        }

    }
}
