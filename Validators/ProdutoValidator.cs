
using FluentValidation;
using APIDesafio.Models;

namespace APIDesafio.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {

        public ProdutoValidator()
        {
            RuleFor(x => x.Titulo).Length(0, 100).WithMessage("Titulo deve conter no maximo 100 caracteres!");


        }
    }
}