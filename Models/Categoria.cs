using System.Collections.Generic;

namespace APIDesafio.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}