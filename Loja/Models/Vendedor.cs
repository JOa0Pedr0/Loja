namespace Loja.Models;

public class Vendedor
{
    public int Id { get; set; }
    public string Nome { get; set; } = null!;
    public double? VendaBruta { get; set; }
    public double ? Comissao { get; set; }
    public ICollection<Cliente>? Clientes { get; set; } = new List<Cliente>();

    public void Informacoes()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Total vendido: {VendaBruta}");
        Console.WriteLine();
    }

}
