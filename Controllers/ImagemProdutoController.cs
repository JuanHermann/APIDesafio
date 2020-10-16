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

namespace APIDesafio.Controllers
{
    [ApiController]
    [Route("imagensprodutos")]
    public class ImagemProdutoController : Controller
    {

        /// <summary>
        /// Busca a imagem de um Produto.
        /// </summary>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get([FromServices] DataContext context, int id)
        {
            var sBytes = (await context.Produtos.FindAsync(id)).Imagem;
            if (sBytes != "") {
                return File(Convert.FromBase64String(sBytes), "image/jpeg");
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Cadastra a imagem de um Produto.
        /// </summary>
        [HttpPost]
        [Route("{id:int}")]
        public async Task<IActionResult> Post([FromServices] DataContext context,  int Id,  IFormFile Imagem)
        {

            string sByte = "";
            if (Imagem.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    Imagem.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    sByte = Convert.ToBase64String(fileBytes);

                }
            }
            context.Produtos.Find(Id).Imagem = sByte;


            await context.SaveChangesAsync();



            return Ok();
        }

        /// <summary>
        /// Atualiza uma Imagem cadastrada.
        /// </summary>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> Put([FromServices] DataContext context,  int Id, [FromForm] IFormFile Imagem)
        {
            string sByte = "";
            if (Imagem.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    Imagem.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    sByte = Convert.ToBase64String(fileBytes);

                }
            }
            context.Produtos.Find(Id).Imagem = sByte;


            await context.SaveChangesAsync();



            return Ok();
        }

        /// <summary>
        /// Exclui uma Imagem.
        /// </summary>
        /// <param name="id"> Id do Produto</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromServices] DataContext context, int id)
        {
            var produto = context.Produtos.Find(id);
            if (produto != null)
            {
                produto.Imagem = "";
                context.Produtos.Remove(produto);
                await context.SaveChangesAsync();
                return Ok("Categoria deletada");
            }
            else
            {
                return NotFound();
            }
        }
    }
}