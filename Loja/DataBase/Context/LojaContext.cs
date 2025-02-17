using Loja.Models;
using Microsoft.EntityFrameworkCore;

namespace Loja.DataBase.Context;

public class LojaContext : DbContext
{
    public DbSet<Cliente>? Clientes { get; set; }
    public DbSet<Fornecedor>? Fornecedores { get; set; }
    public DbSet<Produto>? Produtos { get; set; }
    public DbSet<Vendedor>? Vendedores { get; set; }

    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Loja;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().HasOne(p => p.Fornecedor).WithMany(f => f.Produtos).HasForeignKey(p => p.FornecedorId);

        modelBuilder.Entity<Cliente>().HasOne(c => c.Vendedor).WithMany(v => v.Clientes).HasForeignKey(v => v.VendedorId);


    }
}
