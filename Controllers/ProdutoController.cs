using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDesafio.Data;
using APIDesafio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIDesafio.Validators;

namespace APIDesafio.Controllers
{
    [ApiController]
    [Route("produtos")]
    public class ProdutoController : Controller
    {

        /// <summary>
        /// Busca todas os Produtos cadastradas.
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] DataContext context)
        {
            var produtos = await context.Produtos.ToListAsync();

            return produtos;
        }

        /// <summary>
        /// Cadastra um novo Produto.
        /// </summary>


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Produto>> Post([FromServices] DataContext context, [FromBody] Produto model)
        {
            var validator = new ProdutoValidator();
            if (validator.Validate(model).IsValid)
            {
                context.Produtos.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}