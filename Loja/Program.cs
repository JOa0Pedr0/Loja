using Loja.DataBase.Context;
using Loja.DataBase.Repositories;
using Loja.Menus;
using Loja.Menus_Geral_;
using Loja.Models;
using Microsoft.EntityFrameworkCore;

LojaContext context = new LojaContext();
var repositorioCliente = new RepositorioGenerico<Cliente>(context);

#region Filtro clientes
var filtrarClientesPorVendedor = repositorioCliente.ListarPor(c => c.VendedorId.Equals(2)); //por vendedor
var filtrarClientePorDataCadastro = context.Clientes!.Where(c => c.DataCadastro.Month > 1).Include(c => c.Vendedor).ToList();// por data cadastrada
var filtrarClientePorComprasRealizadas = context.Clientes!.Where(c => c.TotalComprado > 75).Include(c => c.Vendedor).ToList();//por quantiade de compras
foreach (var cliente in filtrarClientesPorVendedor)
{
// Console.WriteLine($"Nome: {cliente.Nome} - Data de cadastro: {cliente.DataCadastro} - Total comprado: {cliente.TotalComprado} - Vendedor responsável: {cliente.Vendedor!.Nome}");
}
#endregion

#region Filtro produtos


#endregion

#region Filtro vendedor
var filtrarVendedorPorVendaBruta = context.Vendedores!.Where(v => v.VendaBruta > 2001).ToList();
//foreach(var vendedor in filtrarVendedorPorVendaBruta)
//{
//   // Console.WriteLine($"Nome: {vendedor.Nome} - Total vendido: {vendedor.VendaBruta} - Comissão: {vendedor.Comissao = vendedor.VendaBruta * 0.04}");
//}
#endregion


//var repositorioVendedor = new RepositorioGenerico<Vendedor>(context);
//Console.WriteLine("Qual o vendedor?");
//string vendedor = Console.ReadLine()!;
//var vendedorAchado = repositorioVendedor.Busca(v => v.Nome.Equals(vendedor));

//var listarClientePorVendedor = repositorioCliente.ListarPor(c => c.Vendedor!.Nome.Equals(vendedor);
//foreach(var cliente in listarClientePorVendedor)
//{
//    Console.WriteLine(cliente.Nome);
//}
var menu = new MenuCliente();
//menu.Menu();
var menuV = new MenuVendedor();
//menuV.Menu();
var menuP = new MenuProduto();
menuP.Menu();
