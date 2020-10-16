using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDesafio.Data;
using APIDesafio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIDesafio.Validators;
using Microsoft.AspNetCore.Http;
using APIDesafio.Interfaces;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Buffers.Text;
using System.Linq;

namespace APIDesafio.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutoController : Controller
    {

        private ProdutoValidator validator;

        public ProdutoController()
        {
            validator = new ProdutoValidator();
        }

        /// <summary>
        /// Busca todos os Produtos cadastrados.
        /// </summary>
        /// <response code="200">Retorna a lista com todos Produtos</response>
        /// <response code="400">Erro na requisição</response>   
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] DataContext context)
        {


            var Produtos = await context.Produtos.ToListAsync();
            Produtos.ForEach(x => { if (x.Imagem != "") { x.Imagem = "Possui imagem salva"; } });

            return Ok(Produtos);
        }

        /// <summary>
        /// Cadastra um novo Produto.
        /// </summary>
        /// <response code="201">Retorna o Produto cadastradao.</response>
        /// <response code="400">Erro no cadastro.</response>            
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Produto>> Post([FromServices] DataContext context, [FromBody] IProduto model)
        {
            var produto = new Produto().IProdutoToProduto(model);
            if (validator.Validate(produto).IsValid)
            {
                context.Produtos.Add(produto);
                await context.SaveChangesAsync();

                return Created("", produto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Atualiza uma Produto cadastrada.
        /// </summary>
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Produto>> Put([FromServices] DataContext context, [FromBody] IProduto model)
        {
            var produto = new Produto().IProdutoToProduto(model);
            if (validator.Validate(produto).IsValid)
            {
                if (context.Produtos.Find(produto.Id) != null)
                {
                    var produtoContext = context.Produtos.Find(model.Id);

                    produtoContext.Id = model.Id;
                    produtoContext.Titulo = model.Titulo;
                    produtoContext.Descricao = model.Descricao;
                    produtoContext.Altura = model.Altura;
                    produtoContext.Largura = model.Largura;
                    produtoContext.Comprimento = model.Comprimento;
                    produtoContext.Peso = model.Peso;
                    produtoContext.CodigoDeBarras = model.CodigoDeBarras;
                    produtoContext.Valor = model.Valor;
                    produtoContext.DataAquisicao = model.DataAquisicao;

                    await context.SaveChangesAsync();
                    return Accepted("Alterado com sucesso");
                }
                return NotFound(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Exclui uma Produto.
        /// </summary>
        /// <param name="id"> Id da Produto</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Produto>> Delete([FromServices] DataContext context, int id)
        {
            var produto = context.Produtos.Find(id);
            if (produto != null)
            {
                context.Produtos.Remove(produto);

                await context.SaveChangesAsync();
                return Ok("Produto deletada");
            }
            else
            {
                return NotFound();
            }
        }






    }
}