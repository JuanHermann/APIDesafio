using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIDesafio.Models
{
    public class CategoriaProduto
    {
        public  CategoriaProduto(int Id,int CategoriaId, int ProdutoId)
        {
            this.Id = Id;
            this.CategoriaId = CategoriaId;
            this.ProdutoId = ProdutoId;
        }
        public CategoriaProduto()
        {
        }


        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
    }
}