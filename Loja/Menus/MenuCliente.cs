using Loja.DataBase.Context;
using Loja.DataBase.Repositories;
using Loja.Menus;
using Loja.Models;
using Loja.Busca;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Loja.Menus_Geral_;
public class MenuCliente : MenuPrincipal
{
        LojaContext context = new LojaContext();
        
    public void Menu()//tratar exceção do usuario digitar uma letra onde é para ser número
    {
        base.LimparTela();

        base.ExibirTexto("Gerenciar cliente");
        Console.WriteLine("\n1 - [Cadastrar]");
        Console.WriteLine("2 - [Atualizar]");
        Console.WriteLine("3 - [Remover]");
        Console.WriteLine("4 - [Listar]");
        Console.WriteLine("5 - [Sair]");
        Console.WriteLine("\nEscolha a opção:");
        int opcao = int.Parse(Console.ReadLine()!);

        switch(opcao)
        {
            case 1:
                Cadastro(context);
                Console.ReadKey();
                Menu();
                break; 

            case 2:
                Atualizar(context);
                Console.ReadKey();
                Menu();
                break;

            case 3:
                Remover(context);
                Console.ReadKey();
                Menu();
                break;

            case 4:
                Listar(context);
                Console.ReadKey();
                Menu();
                break;
            case 5:
                Console.Clear();
                Console.WriteLine("Voltando ao menu principal...");
                break;

           default:

                break;
        }
       
    }

    public void Cadastro(LojaContext context)
    {
        base.LimparTela();
        RepositorioGenerico<Cliente> repositorioCliente = new RepositorioGenerico<Cliente>(context);
        Buscas busca = new Buscas(context);
        base.ExibirTexto("Cadastro cliente");
        Console.WriteLine("\nInforme o nome do cliente:");
        string cliente = Console.ReadLine()!;
        
        var consultaCliente = busca.BuscaCliente(cliente);
        if (consultaCliente is not null)
        {
            base.LimparTela();
            Console.WriteLine($"Já existe o nome {consultaCliente.Nome} cadastrado no sistema!");
        }
        else
        {
            base.LimparTela();
            string resp = "";
            while(resp != "S" && resp != "N")
            {
                base.LimparTela();
                Console.WriteLine($"Deseja cadastrar um vendedor para {cliente}? [S/N]");
                resp = Console.ReadLine()!.ToUpper();
            }
            if (resp == "S")
            {
                base.LimparTela();
                Console.WriteLine("Informe o nome do vendedor:");
                string vendedor = Console.ReadLine()!;
                var consultaVendedor = busca.BuscaVendedor(vendedor);
                RepositorioGenerico<Vendedor> repositorioVendedor = new RepositorioGenerico<Vendedor>(context);
                if (consultaVendedor is not null)
                {
                    base.LimparTela();
                    var novoCliente = new Cliente() { Nome = cliente };
                    novoCliente.Vendedor = consultaVendedor;
                    repositorioCliente.Adicionar(novoCliente);
                    Console.WriteLine("Cadastro finalizado!");
                }
                else
                {
                    base.LimparTela();
                    Console.WriteLine($"Não existe o vendedor {vendedor} no sistema!");
                    resp = " ";
                    while (resp != "S" && resp != "N")
                    {
                        base.PressioneEProssiga();
                        Console.WriteLine("Deseja cadastrar outro vendedor [S/N]");
                        resp = Console.ReadLine()!.ToUpper();
                        if (resp == "S")
                        {
                            while (resp == "S")
                            {
                                base.LimparTela();
                                Console.WriteLine("Informe o nome do vendedor:");
                                vendedor = Console.ReadLine()!;
                                var consultarNovamente = busca.BuscaVendedor(vendedor);
                           
                                if (consultarNovamente is not null)
                                {
                                    base.LimparTela();
                                    var novoCliente = new Cliente() { Nome = cliente };
                                    novoCliente.Vendedor = consultarNovamente;
                                    repositorioCliente.Adicionar(novoCliente);
                                    Console.WriteLine("Cadastro finalizado!");
                                    break;
                                }
                                else
                                {
                                    base.LimparTela();
                                    Console.WriteLine($"Não existe o vendedor {vendedor} no sistema!");
                                    Console.WriteLine("Deseja cadastrar outro vendedor [S/N]");
                                    resp = Console.ReadLine()!.ToUpper();
                                }
                            }
                        }
                        else
                        {
                            base.LimparTela();
                            Console.WriteLine("Tudo bem! O cliente será cadastrado e poderá ser vinculado um vendedor no futuro!");
                            var novoCliente = new Cliente() { Nome = cliente };
                            repositorioCliente.Adicionar(novoCliente);
                            break;
                        }
                    }
                }
            }
            else
            {
                base.LimparTela();
                Console.WriteLine("Cadastro finalizado!");
                var novoCliente = new Cliente() { Nome = cliente };
                repositorioCliente.Adicionar(novoCliente);
                 
            }
        }  
    }
    public void Atualizar(LojaContext context)
    {
        base.LimparTela();
        Buscas busca = new Buscas(context);
        base.ExibirTexto("Atualizar dados do cliente");
        Console.WriteLine("\nInforme o nome do cliente:");
        string cliente = Console.ReadLine()!;
        base.LimparTela();
        var consultaCliente = busca.BuscaCliente(cliente);
        if (consultaCliente is not null)
        {
            AtualizarP2(consultaCliente);
        }
        else
        {
            Console.WriteLine($"O nome {cliente} não foi encontrardo!");
        }
    }
    public void AtualizarP2(Cliente cliente)
    {
        RepositorioGenerico<Cliente> clienteRepositorio = new (context);
        base.LimparTela();
        Buscas buscas = new Buscas(context);
        Console.WriteLine("1 - Para alterar o nome ");
        Console.WriteLine("2 - Para alterar o vendedor");
        Console.WriteLine("3 - Sair");
       
            int opcao = int.Parse(Console.ReadLine()!);
            while (opcao != 1 && opcao != 2 && opcao != 3)
            {
              Console.WriteLine("Opção inválida!");
              base.PressioneEProssiga();
              AtualizarP2(cliente);
            }
            switch (opcao)
            {
                case 1:
                    base.LimparTela();
                    base.ExibirTexto("Alterar nome do cliente");
                    
                    Console.WriteLine("\nInforme o novo nome:");
                    string novoNome = Console.ReadLine()!;
                    var buscaCliente = buscas.BuscaCliente(novoNome);
                    if(buscaCliente is not null)
                    {
                        base.LimparTela();
                        Console.WriteLine($"Já existe o nome {buscaCliente.Nome} cadastrado no sistema!");
                    }
                    else
                    {
                     base.LimparTela();
                     cliente.Nome = novoNome;
                     clienteRepositorio.Atualizar(cliente);
                     Console.WriteLine("O nome foi atualizado!");
                    }
                    break;

                case 2:
                     base.LimparTela();
                     base.ExibirTexto("Alterar vendedor do cliente");
                     Console.WriteLine("Informe o nome do vendedor:");
                     string vendedor = Console.ReadLine()!;
                     Buscas buscaVendedor = new Buscas(context);
                     var consultaVendedor = buscaVendedor.BuscaVendedor(vendedor);
                if (consultaVendedor is not null)
                {
                    if(cliente.VendedorId == consultaVendedor.Id)
                    {
                        base.LimparTela();
                        Console.WriteLine($"O vendedor já está vinculado a este cliente!");
                    }
                    else
                    {
                        base.LimparTela();
                        cliente.Vendedor = consultaVendedor;
                        Console.WriteLine($"Vendedor {consultaVendedor.Nome} foi vinculado a {cliente.Nome}");
                        clienteRepositorio.Atualizar(cliente);
                    }
                }
                else
                {
                    base.LimparTela();
                    Console.WriteLine($"Nome {vendedor} não foi encontrado!");
                }
                    break;
                case 3:
                     base.LimparTela();
                     Console.WriteLine("Voltando ao menu principal");
                    break;

                default:
                    break;
            }
        }
    public void Remover(LojaContext context)
    {
        base.LimparTela();
        RepositorioGenerico<Cliente> repositorioCliente = new (context);
        Buscas busca = new Buscas (context);
        base.ExibirTexto("Excluir cliente");
        Console.WriteLine("\nInforme o nome do cliente que deseja excluir:");
        string cliente = Console.ReadLine()!;
        var consultaCliente = busca.BuscaCliente(cliente);
        if (consultaCliente is not null)
        {
            base.LimparTela();
            string resp = "";
            while(resp != "S" && resp != "N")
            {
                base.LimparTela();
                Console.WriteLine($"Tem certeza que deseja excluir o cliente {consultaCliente.Nome} - [S/N] ? (Todos os dados serão excluidos!)");
                resp = Console.ReadLine()!.ToUpper();
            }
            if (resp == "S")
            {
                base.LimparTela();
                base.ExibirTexto($"Exluindo dados de {consultaCliente.Nome}...");
                repositorioCliente.Excluir(consultaCliente);
            }
            else
            {
                base.LimparTela();
                Console.WriteLine("Voltando ao menu!");
            }
           
        }
        else
        {
            base.LimparTela();
            Console.WriteLine($"O nome {cliente} não foi encontrado no banco de dados");
        }
 
    }
    public void Listar(LojaContext context)
    {
        base.LimparTela();
        RepositorioGenerico<Cliente> repositorioCliente = new (context);
        Buscas busca = new Buscas(context);
        int opcao = 0;
        while(opcao != 1 && opcao != 2 && opcao != 3 && opcao != 4)
        {
            try
            {
                base.ExibirTexto("Listar cliente cadastrados");
                Console.WriteLine("\n1 - [Listagem A - Z]");
                Console.WriteLine("2 - [Listar por data de cadastro]");
                Console.WriteLine("3 - [Listar por maior número de compras]");
                Console.WriteLine("4 - [Listar por vendedor]");
                Console.WriteLine("5 - [Sair]");
                Console.WriteLine("\n Digite a opção escolhida:");
                opcao = int.Parse(Console.ReadLine()!);
                base.LimparTela();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Opção inválida");
                base.PressioneEProssiga();
            }
        }
        switch (opcao)
        {
            case 1:
                base.LimparTela();
                base.ExibirTexto("Imprimindo lista de clientes em ordem alfabética:");
                Console.WriteLine("");
                var listaDeClientes =  repositorioCliente.ListarCliente();
                if(listaDeClientes is not null || listaDeClientes.Count() > 0)
                {
                    foreach (var clientes in listaDeClientes)
                    {
                        clientes.Informacoes();
                    }
                }
                else
                {
                    Console.WriteLine("A lista se encontra vazia!");
                }
                break;

            case 2:
                base.LimparTela();
                Console.WriteLine("\nInforme o ano que deseja filtrar:");
                int anoDeBusca = int.Parse(Console.ReadLine()!);
                base.LimparTela();
                base.ExibirTexto($"Imprimindo lista de clientes do ano de {anoDeBusca}:");
                Console.WriteLine("");

                var listaDeClientePorData = repositorioCliente.ListarPor(c => c.DataCadastro.Year.Equals(anoDeBusca));
                if (listaDeClientePorData is null || listaDeClientePorData.Count() == 0)
                {
                    base.LimparTela();
                    Console.WriteLine("A lista encontra-se vazia!");
                }
                else
                {
                    foreach (var clientes in listaDeClientePorData)
                    {
                        Console.WriteLine($"Nome: {clientes.Nome} - Data de cadastro: {clientes.DataCadastro.Day}/{clientes.DataCadastro.Month}/{clientes.DataCadastro.Year}");
                    }
                }
                break;

            case 3:
                base.LimparTela();
                base.ExibirTexto("Imprimindo lista de cliente por maior número de compras:");
                Console.WriteLine("");
                var listaDeClientesPorCompras = repositorioCliente.Listar().OrderByDescending(c => c.TotalComprado);
                if (listaDeClientesPorCompras is null || listaDeClientesPorCompras.Count() == 0) 
                {
                    base.LimparTela();
                    Console.WriteLine("A lista encotra-se vazia!");
                }
                else
                {
                    foreach (var clientes in listaDeClientesPorCompras)
                    {
                        Console.WriteLine($"Nome: {clientes.Nome} - Total comprado: {clientes.TotalComprado}");
                        Console.WriteLine("-----------------------------------------------------------------");
                    }
                }
                break;
            case 4:
                base.LimparTela();
                string resp = "S";
                while (resp == "S")
                {
                    Console.WriteLine("Informe o nome do vendedor:");
                    string vendedor = Console.ReadLine()!;
                    var buscaVendedor = busca.BuscaVendedor(vendedor);
                    if (buscaVendedor is not null)
                    {
                        var clienteVendedor = repositorioCliente.ListarPor(c => c.VendedorId.Equals(buscaVendedor.Id));
                        base.LimparTela();
                        base.ExibirTexto($"Imprimindo lista de clientes do vendedor {vendedor}:");
                        if( clienteVendedor is not null && clienteVendedor!.Count() > 0)
                        {
                            foreach (var clientes in clienteVendedor)
                            {
                                Console.WriteLine($"Nome: {clientes.Nome}");
                                Console.WriteLine("--------------------------------");
                            }
                        }
                        else
                        {
                            base.LimparTela();
                            Console.WriteLine("A lista se encontra vazia!");
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"O vendedor {vendedor} não foi encontrado!");
                        Console.WriteLine("Deseja buscar novamente [S/N]?");
                        resp = Console.ReadLine()!;
                    }
                }
                    break;
            case 5:
                base.LimparTela();
                Console.WriteLine("Voltando ao menu");
                break;
            default:
                base.LimparTela();
                Console.WriteLine("Opção inválida!");
                break;
        }
    }
     
    }

