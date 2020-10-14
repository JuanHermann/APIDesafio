using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace APIDesafio.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Medida Medidas { get; set; }
        public Double Peso { get; set; }
        public BigInteger CodigoDeBarras { get; set; }
        public Double Valor { get; set; }
        public DateTime DataAquisicao { get; set; }
        public string Imagem { get; set; }


    }

}