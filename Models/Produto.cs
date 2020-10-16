using APIDesafio.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace APIDesafio.Models
{
    public class Produto
    {

        [Key]
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
        public string Imagem { get; set; }

        public Produto IProdutoToProduto(IProduto model)
        {
            return new Produto()
            {
                Id = model.Id,
                Titulo = model.Titulo,
                Descricao = model.Descricao,
                Altura = model.Altura,
                Largura = model.Largura,
                Comprimento = model.Comprimento,
                Peso = model.Peso,
                CodigoDeBarras = model.CodigoDeBarras,
                Valor = model.Valor,
                DataAquisicao = model.DataAquisicao,
                Imagem = ""
                
            };
        }



    }

}