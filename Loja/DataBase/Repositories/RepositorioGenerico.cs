using Loja.DataBase.Context;
using Loja.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Loja.DataBase.Repositories;

public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
{
    private readonly LojaContext _lojaContext;
    private readonly DbSet<T> _dbSet;

    public RepositorioGenerico(LojaContext lojaContext)
    {
        _lojaContext = lojaContext;
        _dbSet = _lojaContext.Set<T>();
    }

    public void Adicionar(T objeto)
    {
        _dbSet.Add(objeto);
        _lojaContext.SaveChanges();
    }

    public void Atualizar(T objeto)
    {
        _dbSet.Update(objeto);
        _lojaContext.SaveChanges();
    }

    public void Excluir(T objeto)
    {
        _dbSet.Remove(objeto);
        _lojaContext.SaveChanges();
    }

    public IEnumerable<T> Listar()
    {
        return _dbSet.ToList();
    }

    public T? Busca(Func<T, bool> objeto)
    {
        return _dbSet.FirstOrDefault(objeto);
    }

    public IEnumerable<T> ListarPor(Expression<Func<T, bool>> expression)
    {
        return _dbSet.Where(expression).ToList();
    }
    public IEnumerable<Cliente> ListarCliente()
    {
        return _lojaContext.Clientes!.Include(c => c.Vendedor).ToList();
    }
}
