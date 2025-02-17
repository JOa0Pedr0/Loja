namespace Loja.Models;

public class Produto
{
    public int Id { get; set; }
    public int FornecedorId { get; set; }
    public string Nome { get; set; } = null!;
    public double Preco { get; set; }
    public Fornecedor? Fornecedor { get; set; } 

}
