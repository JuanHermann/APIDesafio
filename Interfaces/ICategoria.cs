using APIDesafio.Models;
using System.Collections.Generic;

namespace APIDesafio.Interfaces

{
    public class ICategoria
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<Produto> Pordutos { get; set; }
    }
}