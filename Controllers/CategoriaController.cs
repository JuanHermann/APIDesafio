using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDesafio.Data;
using APIDesafio.Models;
using APIDesafio.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace APIDesafio.Controllers
{
    /// <summary>
    /// Operações com s Categoria 
    /// </summary>
    [ApiController]
    [Route("categorias")]
    [Authorize]
    public class CategoriaController : Controller
    {
        private CategoriaValidator validator;

        public CategoriaController()
        {
            validator = new CategoriaValidator();
        }

        /// <summary>
        /// Busca uma Categoria cadastrada conforme o id enviado.
        /// </summary>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> Get([FromServices] DataContext context, int id)
        {
            var categoria = await context.Categorias.FindAsync(id);
            if (categoria != null)
            { return Ok(categoria); }
            else {
                return NotFound();
            }
        }

        /// <summary>
        /// Busca todas as Categorias cadastradas.
        /// </summary>
        /// <response code="200">Retorna a lista com todas Categorias</response>
        /// <response code="400">Erro na requisição</response>   
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] DataContext context)
        {
            var categorias = await context.Categorias.ToListAsync();

            return Ok(categorias);
        }

        /// <summary>
        /// Cadastra uma nova Categoria.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     {
        ///        "Titulo": "Alimento"
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>A nova Categoria cadastrada</returns>
        /// <response code="201">Retorna Categoria cadastrada.</response>
        /// <response code="400">Erro no cadastro.</response>            
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Categoria>> Post([FromServices] DataContext context, [FromBody] Categoria model)
        {
            if (validator.Validate(model).IsValid)
            {
                context.Categorias.Add(model);
                await context.SaveChangesAsync();

                return Created("", model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Atualiza uma Categoria cadastrada.
        /// </summary>
        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Categoria>> Put([FromServices] DataContext context, [FromBody] Categoria model)
        {

            if (validator.Validate(model).IsValid)
            {
                if (context.Categorias.Find(model.Id) != null)
                {
                    var categoria =  context.Categorias.Find(model.Id);

                    categoria.Titulo = model.Titulo;

                    await context.SaveChangesAsync();
                    return Ok(model);
                }
                return NotFound(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Exclui uma Categoria.
        /// </summary>
        /// <param name="id"> Id da Categoria</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> Delete([FromServices] DataContext context, int id)
        {
            var categoria = context.Categorias.Find(id);
            if (categoria != null)
            {
                context.Categorias.Remove(categoria);
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