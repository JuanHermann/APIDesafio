using APIDesafio.Models;
using System;
using System.Collections.Generic;

namespace APIDesafio.Interfaces

{
    public class IProduto
    {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }
        public Double Peso { get; set; }
        public Decimal CodigoDeBarras { get; set; } 
        public Double Valor { get; set; }
        public DateTime DataAquisicao { get; set; } = DateTime.Now;
    }
}