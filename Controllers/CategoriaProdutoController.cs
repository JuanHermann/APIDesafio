using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDesafio.Data;
using APIDesafio.Models;
using APIDesafio.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace APIDesafio.Controllers
{
    /// <summary>
    /// Operações com o Aluno 
    /// </summary>
    [ApiController]
    [Route("categoriasprodutos")]
    public class CategoriaProdutoController : Controller
    {


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
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     {
        ///        "Id": 0,
        ///        "Titulo": "Alimento"
        ///     }
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A nova CategoriaProduto cadastrada</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaProduto>> Post([FromServices] DataContext context, [FromBody] CategoriaProduto model)
        {
            var validator = new CategoriaProdutoValidator();
            if (validator.Validate(model).IsValid)
            {
                context.CategoriasProdutos.Add(model);
                await context.SaveChangesAsync();
                return model;
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
            var validator = new CategoriaProdutoValidator();
            if (validator.Validate(model).IsValid)
            {
                if (context.CategoriasProdutos.Find(model.Id) != null)
                {
                    context.CategoriasProduto.Find(model.Id).Titulo = model.Titulo;
                    await context.SaveChangesAsync();
                    return "Alterado com sucesso";
                }
                return "Categoria não encontrada";
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
            var categoriaProduto = context.CategoriasProdutos.Find(id);
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