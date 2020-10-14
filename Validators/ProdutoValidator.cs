
using FluentValidation;
using APIDesafio.Models;
using FluentValidation.Validators;

namespace APIDesafio.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {

        public ProdutoValidator()
        {
            RuleFor(x => x.Titulo).Length(0, 100).WithMessage("Titulo deve conter no maximo 100 caracteres!");
            RuleFor(x => x.Peso).SetValidator(new ScalePrecisionValidator(2, 4));

        }
    }
}