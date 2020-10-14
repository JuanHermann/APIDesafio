using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIDesafio.Models
{
    public class CategoriaProduto
    {

        [Key]
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public Categoria Categoria { get; set; }
    }
}