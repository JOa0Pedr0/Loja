using System.Linq.Expressions;

namespace Loja.DataBase.Repositories;

public interface IRepositorioGenerico<T> where T : class
{
    void Adicionar(T objeto);
    void Atualizar(T objeto);
    void Excluir(T objeto);
    IEnumerable<T> Listar();
    IEnumerable<T> ListarPor(Expression<Func<T,bool>> expression);
}
