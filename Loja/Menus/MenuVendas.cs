using Loja.Busca;
using Loja.DataBase.Context;
using Loja.DataBase.Repositories;
using Loja.Models;

namespace Loja.Menus
{
    public class MenuVendas : MenuPrincipal
    {
        /*
         * Realizar venda 
         * Informar o cliente
         * atualizar status a cada venda [cliente e vendedor]
         * cancelar uma venda
         */
        LojaContext lojaContext = new LojaContext();
        public void Menu()
        {
            base.LimparTela();
            Console.Clear();
            base.ExibirTexto("Menu vendas");
            Console.WriteLine("Informe o nome do cliente");


        }
        public void SomarProdutos(LojaContext context)
        {
            RepositorioGenerico<Produto> repositorioProduto = new (lojaContext);
            Buscas busca = new Buscas(lojaContext);
            base.LimparTela();
            Console.WriteLine("");
            string nomeProduto = Console.ReadLine()!;
            var tot = 0;

        }

    }
}
