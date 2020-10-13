using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDesafio.Data;
using APIDesafio.Models;
using APIDesafio.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace APIDesafio.Controllers
{
    [ApiController]
    [Route("categorias")]
    public class CarController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] DataContext context)
        {
            var categorias = await context.Categorias.ToListAsync();

            return categorias;
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> Post([FromServices] DataContext context, [FromBody] Categoria model)
        {
            var validator = new CategoriaValidator();
            if (validator.Validate(model).IsValid)
            {
                context.Categorias.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Produto>> Put([FromServices] DataContext context, [FromBody] Produto model)
        {
            if (ModelState.IsValid)
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

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<Produto>> Delete([FromServices] DataContext context, [FromBody] Produto model)
        {
            if (ModelState.IsValid)
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