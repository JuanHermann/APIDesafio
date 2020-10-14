using Microsoft.EntityFrameworkCore;
using APIDesafio.Models;

namespace APIDesafio.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> optionsBuilder) : base(optionsBuilder)
        { }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<CategoriaProduto> CategoriasProdutos { get; set; }

        public DbSet<User> Users { get; set; }


    }
}