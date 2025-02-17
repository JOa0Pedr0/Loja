namespace Loja.Models;

public class Cliente
{
    public int Id { get; set; }
    public int ? VendedorId { get; set; }
    public string Nome { get; set; } = null!;
    public DateTimeOffset DataCadastro { get; set; } = DateTimeOffset.Now;
    public Vendedor? Vendedor { get; set; } //one-to-one -> Cliente-Vendedor
    public double TotalComprado { get; set; } = 0;


    public void Informacoes()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Data de cadastro: {DataCadastro.ToString("dd/MM/yyyy")}");
        Console.WriteLine($"Vendedor: {Vendedor?.Nome}");
        Console.WriteLine($"Total comprado: {TotalComprado}");
        Console.WriteLine("----------------------------------------------------------");


    }

}
