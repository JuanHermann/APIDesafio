using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDesafio.Data;
using APIDesafio.Models;
using APIDesafio.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace APIDesafio.Controllers
{
    /// <summary>
    /// Operações com o Aluno 
    /// </summary>
    [ApiController]
    [Route("categoriasprodutos")]
    [Authorize]
    public class CategoriaProdutoController : Controller
    {
        private CategoriaProdutoValidator validator;

        public CategoriaProdutoController()
        {
            validator = new CategoriaProdutoValidator();
        }


        /// <summary>
        /// Busca uma CategoriaProdutos cadastradas conforme o id enviado.
        /// </summary>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CategoriaProduto>> Get([FromServices] DataContext context,int id )
        {
            var categoriaProdutos = await context.CategoriasProdutos.FindAsync(id);

            if (categoriaProdutos != null)
            { return Ok(categoriaProdutos); }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Busca todas as CategoriaProdutos cadastradas.
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<CategoriaProduto>>> Get([FromServices] DataContext context)
        {
            var categoriaProdutos = await context.CategoriasProdutos.ToListAsync();

            return categoriaProdutos;
        }

        /// <summary>
        /// Cadastra uma nova CategoriaProduto.
        /// </summary>
        /// <returns>A nova CategoriaProduto cadastrada</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [Route("{categoriaId:int}/{produtoId:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaProduto>> Post([FromServices] DataContext context, int categoriaId,int produtoId)
        {
            var categoriaProduto = new CategoriaProduto(0, categoriaId, produtoId);
            if (validator.Validate(categoriaProduto).IsValid)
            {
                context.CategoriasProdutos.Add(categoriaProduto);
                await context.SaveChangesAsync();
                return categoriaProduto;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Atualiza uma CategoriaProduto cadastrada.
        /// </summary>
        [HttpPut]
        [Route("")]
        public async Task<string> Put([FromServices] DataContext context, [FromBody] CategoriaProduto model)
        {
            if (validator.Validate(model).IsValid)
            {
                if (context.CategoriasProdutos.Find(model.Id) != null)
                {
                    var categoriaProduto = await context.CategoriasProdutos.FindAsync(model.Id);
                    categoriaProduto.CategoriaId = model.CategoriaId;
                    categoriaProduto.ProdutoId = model.ProdutoId;

                    await context.SaveChangesAsync();
                    return "Alterado com sucesso";
                }
                return "CategoriaProduto não encontrada";
            }
            else
            {
                return ModelState.ToString();
            }
        }

        /// <summary>
        /// Exclui uma CategoriaProduto.
        /// </summary>
        /// <param name="id"> Id da CategoriaProduto</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<string> Delete([FromServices] DataContext context, int id)
        {
            var categoriaProduto = await context.CategoriasProdutos.FindAsync(id);
            if (categoriaProduto != null)
            {
                context.CategoriasProdutos.Remove(categoriaProduto);
                await context.SaveChangesAsync();
                return "CategoriaProduto deletada";
            }
            else
            {
                return "CategoriaProduto não encontrada";
            }
        }



    }
}