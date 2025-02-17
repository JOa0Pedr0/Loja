using Loja.DataBase.Repositories;
using Loja.Models;

namespace Loja.Menus;

public class MenuPrincipal
{
    public void LimparTela()
    {
        Console.Clear();
    }
    public void ExibirTexto(string txt)
    {
        int qntLetras = txt.Length;
        string asterisco = string.Empty.PadLeft(qntLetras, '*');
        Console.WriteLine(asterisco);
        Console.WriteLine(txt);
        Console.WriteLine(asterisco);
    }
    public void PressioneEProssiga()
    {
        Console.WriteLine("Pressione qualquer tecla para prosseguir");
        Console.ReadKey();
        Console.Clear();



    }
    
}
