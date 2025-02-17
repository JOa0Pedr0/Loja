namespace Loja.Models;

public class Fornecedor
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public ICollection<Produto> ? Produtos { get; set; }// one-to-many com  Produto
}
