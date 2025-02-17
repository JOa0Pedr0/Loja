using Loja.Busca;
using Loja.DataBase.Context;
using Loja.DataBase.Repositories;
using Loja.Models;
using Microsoft.Identity.Client;

namespace Loja.Menus
{
    public class MenuVendedor : MenuPrincipal
    {
       LojaContext context = new LojaContext();

        public void  Menu()
        {
         
            int opcao = 7;
            while(opcao != 1 && opcao != 2 && opcao != 3 && opcao != 4 && opcao != 5)
            {
                
                try
                {
                    base.LimparTela();
                    base.ExibirTexto("Gerenciar vendedor");
                    Console.WriteLine("\n1 - [Cadastrar]");
                    Console.WriteLine("2 - [Atualizar]");
                    Console.WriteLine("3 - [Remover]");
                    Console.WriteLine("4 - [Listar]");
                    Console.WriteLine("5 - [Sair]");
                    Console.WriteLine("\n Digite uma das opções válidas:");
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
                    base.LimparTela();
                    Cadastro(context);
                    Console.ReadKey();
                    Menu();
                    break;

                case 2:
                    base.LimparTela();
                    Atualizar(context);
                    Console.ReadKey();
                    Menu();
                    break;

                case 3:
                    base.LimparTela();
                    Remover(context);
                    Console.ReadKey();
                    Menu();
                    break;

                case 4:
                    base.LimparTela();
                    Listar(context);
                    Console.ReadKey();
                    Menu();
                    break;

                case 5:
                    Console.WriteLine("Voltando ao menu principal......");
                    break;
            }
           
           
            

        }

        public void Cadastro(LojaContext context)
        {
            RepositorioGenerico<Vendedor> repositorioVendedor = new(context);
            Buscas busca = new Buscas(context);
            base.LimparTela();
            base.ExibirTexto("Registrar vendedor");
            Console.WriteLine("\nInforme o nome do vendedor:");
            string nome = Console.ReadLine()!;
            var consultarVendedor = busca.BuscaVendedor(nome);
            if(consultarVendedor != null)
            {
                base.LimparTela();
                Console.WriteLine($"O nome {consultarVendedor.Nome} já existe!");
            }
            else
            {
                base.LimparTela();
                Vendedor novoVendedor = new Vendedor() {Nome = nome};
                Console.WriteLine($"{nome} foi cadastrado com sucesso!");
                repositorioVendedor.Adicionar(novoVendedor);
            }
        }
        public void Atualizar(LojaContext context)
        {
            RepositorioGenerico<Vendedor> repositorioVendedor = new(context);
            Buscas busca = new Buscas(context);
            base.LimparTela();
            base.ExibirTexto("Atualizar dados do vendedor");
            Console.WriteLine("Informe o nome do vendedor que deseja alterar:");
            string nomeVendedor = Console.ReadLine()!;
            var consultaVendedor = busca.BuscaVendedor(nomeVendedor);
            if (consultaVendedor != null)
            {
                base.LimparTela();
                Console.WriteLine("Informe o novo nome para o vendedor:");
                string novoNomeVendedor = Console.ReadLine()!;
                var novoNome = busca.BuscaVendedor(novoNomeVendedor);
                if(novoNome is null)
                {
                    base.LimparTela();
                    consultaVendedor.Nome = novoNomeVendedor;
                    repositorioVendedor.Atualizar(consultaVendedor);
                    Console.WriteLine("O nome foi alterado com sucesso!");
                }
                else
                {
                    base.LimparTela();
                    Console.WriteLine($"O nome {novoNome.Nome} já existe!");
                }

            }
            else
            {
                base.LimparTela();
                Console.WriteLine($"O nome {nomeVendedor} não foi encontrado!");
            }

        }
        public void Remover(LojaContext context)
        {
            base.LimparTela();
            RepositorioGenerico<Vendedor> repositorioVendedor = new(context);
            Buscas busca = new Buscas(context);
            base.LimparTela();
            ExibirTexto("Remover vendedor do sistema");
            Console.WriteLine("\nInforme o nome do vendedor que deseja remover do sistema:");
            string nomeVendedor = Console.ReadLine()!;
            var consultavendedor =  busca.BuscaVendedor(nomeVendedor);
            if(consultavendedor is not null)
            {
                base.LimparTela();
                string resp = "";
                while (resp != "S" && resp != "N")
                {
                    base.LimparTela();
                    Console.WriteLine($"Tem certeza que deseja excluir o cliente {consultavendedor.Nome} - [S/N] ? (Todos os dados serão excluidos!)");
                    resp = Console.ReadLine()!.ToUpper();
                }
                if (resp == "S")
                {
                    base.LimparTela();
                    base.ExibirTexto($"Exluindo dados de {consultavendedor.Nome}...");
                    repositorioVendedor.Excluir(consultavendedor);
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
                Console.WriteLine($"O nome {nomeVendedor} não foi encontrado!");
            }

        }
        public void Listar(LojaContext context)
        {
            RepositorioGenerico<Vendedor> repositorioVendedor = new (context);
            base.LimparTela();
            int opcao = 0;
            while (opcao != 1 &&  opcao != 2 && opcao != 3)
            {
                try
                {
                    Thread.Sleep(1000);
                    base.LimparTela();
                    base.ExibirTexto("Listar Vendedores!");
                    Console.WriteLine(" 1 - [Listagem de A - Z]");
                    Console.WriteLine(" 2 - [Listagem por maior número de vendas]");
                    Console.WriteLine(" 3 - [Sair]");
                    Console.WriteLine("\nInforme a opação escolhida:");
                    opcao = int.Parse(Console.ReadLine()!);
                    base.LimparTela();

                }catch (Exception ex)
                {
                    Console.WriteLine("Opção inválida!");
                }
            }
            switch (opcao)
            {
                case 1:
                    var listaVendedor = repositorioVendedor.Listar();
                    foreach(var vendedor in listaVendedor)
                    {
                        vendedor.Informacoes();
                    }
                    break;

                case 2:
                    var listaPorVendas = repositorioVendedor.Listar().OrderByDescending(v => v.VendaBruta);
                    foreach(var vendedor in listaPorVendas)
                    {
                        vendedor.Informacoes();
                    }
                    break;
                case 3:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção Inválida!");
                    break;
            }
        }
    }
}
