using System;
using System.Collections.Generic;

namespace APIDesafio.Models
{
    public class Produto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Medidas Medidas { get; set; }
        public Double Peso { get; set; }
        public string CodigoDeBarras { get; set; }
        public ICollection<Categoria> Categorias { get; set; }
        public Double Valor { get; set; }
        public DateTime DataAquisicao { get; set; }
        public string Imagem { get; set; }


    }

}