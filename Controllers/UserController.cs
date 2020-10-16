using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIDesafio.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using APIDesafio.Services;
using APIDesafio.Data;
using Microsoft.EntityFrameworkCore;

namespace APIDesafio.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, string login, string senha)
        {
            var userLogin = new User
            {
                Id = 0,
                UserName = login,
                Password = senha,
                Role = "employee"
            };
            var users = await context.Users.ToListAsync();
            var user = users.Where(x => x.UserName.ToLower() == userLogin.UserName.ToLower() && x.Password == userLogin.Password).FirstOrDefault();

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user,
                token
            };
        }

        [HttpPost]
        [Route("CreateLogin")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Post([FromServices] DataContext context, string login, string senha)
        {
            var user = new User
            {
                Id = 0,
                UserName = login,
                Password = senha,
                Role = "employee"
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Created("", user);

        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";

    }
}