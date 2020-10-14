using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIDesafio.Models
{
    public class Categoria
    {

        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
    }
}