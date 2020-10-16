using System;
using Microsoft.AspNetCore.Mvc;
using APIDesafio.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace APIDesafio.Controllers
{
    [ApiController]
    [Route("imagensprodutos")]
    [Authorize]
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
            if (sBytes != "")
            {
                return File(Convert.FromBase64String(sBytes), "image/jpeg");
            }
            else
            {
                return BadRequest("Produto sem imagem ou não temos produto com esse ID.");
            }
        }

        /// <summary>
        /// Cadastra a imagem de um Produto.
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromServices] DataContext context, int Id, IFormFile Imagem)
        {
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
                var produto = context.Produtos.Find(Id);
                if (produto != null)
                {
                    produto.Imagem = sByte;
                    await context.SaveChangesAsync();
                    return Ok("Imagem Salva");
                }
                else
                {
                    return NotFound();
                }
            }
        }


        /// <summary>
        /// Atualiza uma Imagem cadastrada.
        /// </summary>
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Put([FromServices] DataContext context, int Id, IFormFile Imagem)
        {
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
                var produto = context.Produtos.Find(Id);
                if (produto != null)
                {
                    produto.Imagem = sByte;
                    await context.SaveChangesAsync();
                    return Ok("Imagem Alterada");
                }
                else
                {
                    return NotFound();
                }
            }
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
                return Ok("Imagem descadastrada");
            }
            else
            {
                return NotFound();
            }
        }


    }
}