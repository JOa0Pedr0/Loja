using Loja.DataBase.Context;
using Loja.DataBase.Repositories;
using Loja.Models;

namespace Loja.Busca;

public class Buscas
{
    private readonly LojaContext _context = new LojaContext();

    public Buscas(LojaContext context)
    {
        _context = context;
    }

    public Cliente ? BuscaCliente(string cliente)
    {
        RepositorioGenerico<Cliente> repositorioCliente = new RepositorioGenerico<Cliente>(_context);
        var buscaCliente = repositorioCliente.Busca(c => c.Nome.Equals(cliente));
        return buscaCliente;
    }
    public Vendedor ? BuscaVendedor(string vendedor)
    {
        RepositorioGenerico<Vendedor> repositorioVendedor = new RepositorioGenerico<Vendedor>(_context);
        var buscaVendedor = repositorioVendedor.Busca(v => v.Nome.Equals(vendedor));
        return buscaVendedor;
    }
    public Produto ? BuscaProduto(string produto)
    {
        RepositorioGenerico<Produto> repositorioProduto = new RepositorioGenerico<Produto>(_context);
        var buscaProduto = repositorioProduto.Busca(p => p.Nome.Equals(produto));
        return buscaProduto;
    }
    public Fornecedor ? BuscaFornecedor(string fornecedor)
    {
        RepositorioGenerico<Fornecedor> repositorioFornecedor = new RepositorioGenerico<Fornecedor>(_context);
        var buscarFornecedor = repositorioFornecedor.Busca(f => f.Nome.Equals(fornecedor));
        return buscarFornecedor;
    }
}
