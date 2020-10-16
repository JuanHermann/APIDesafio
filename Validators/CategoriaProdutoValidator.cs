
using FluentValidation;
using APIDesafio.Models;

namespace APIDesafio.Validators
{
    public class CategoriaProdutoValidator : AbstractValidator<CategoriaProduto>
    {

        public CategoriaProdutoValidator()
        {
            RuleFor(x => x.CategoriaId).NotNull().WithMessage("CategoriaId não pode ser null");
            RuleFor(x => x.ProdutoId).NotNull().WithMessage("ProdutoId não pode ser null");
            RuleFor(x => x.CategoriaId).GreaterThan(1).WithMessage("CategoriaId não pode ser menor que 1");
            RuleFor(x => x.ProdutoId).GreaterThan(1).WithMessage("ProdutoId não pode ser menor que 1");
        }
    }
}