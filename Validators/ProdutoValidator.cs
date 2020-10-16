
using FluentValidation;
using APIDesafio.Models;
using FluentValidation.Validators;
using System;
using APIDesafio.Interfaces;

namespace APIDesafio.Validators
{
    public class ProdutoValidator : AbstractValidator<IProduto>
    {

        public ProdutoValidator()
        {
            RuleFor(x => x.Titulo).Length(0, 100).WithMessage("Titulo deve conter no maximo 100 caracteres!");
            RuleFor(x => x.DataAquisicao.Date).LessThan(DateTime.Now.Date.AddDays(1)).WithMessage("DataAquisicao não pode ser maior que a data de hoje!");
            RuleFor(x => x.Peso).SetValidator(new ScalePrecisionValidator(2, 4)).WithMessage("Maximo de 4 numeros após a virgula!");

        }
    }
}