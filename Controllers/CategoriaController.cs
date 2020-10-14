﻿using Microsoft.AspNetCore.Mvc;
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
    /// Operações com s Categoria 
    /// </summary>
    [ApiController]
    [Route("categorias")]
    public class CategoriaController : Controller
    {


        /// <summary>
        /// Busca todas as Categorias cadastradas.
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] DataContext context)
        {
            var categorias = await context.Categorias.ToListAsync();

            return categorias;
        }

        /// <summary>
        /// Cadastra uma nova Categoria.
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
        /// <returns>A nova Categoria cadastrada</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Atualiza uma Categoria cadastrada.
        /// </summary>
        [HttpPut]
        [Route("")]
        public async Task<string> Put([FromServices] DataContext context, [FromBody] Categoria model)
        {
            var validator = new CategoriaValidator();
            if (validator.Validate(model).IsValid)
            {
                if (context.Categorias.Find(model.Id) != null)
                {
                    context.Categorias.Find(model.Id).Titulo = model.Titulo;
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
        /// Exclui uma Categoria.
        /// </summary>
        /// <param name="id"> Id da Categoria</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<string> Delete([FromServices] DataContext context, int id)
        {
            var categoria = context.Categorias.Find(id);
            if (categoria != null)
            {
                context.Categorias.Remove(categoria);
                await context.SaveChangesAsync();
                return "Categoria deletada";
            }
            else
            {
                return "Categoria não encontrada";
            }
        }



    }
}