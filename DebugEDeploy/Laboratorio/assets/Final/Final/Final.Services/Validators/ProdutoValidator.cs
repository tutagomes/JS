using System;
using FluentValidation;
using Final.Domain.Entities;
namespace Final.Service.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
{
	public ProdutoValidator()
        {
	    RuleFor(c => c)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Can't found the object.");
                });
		
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Is necessary to inform the Name.")
                .NotNull().WithMessage("Is necessary to inform the Name.");

            RuleFor(c => c.Codigo)
                .NotEmpty().WithMessage("Is necessary to inform the Codigo.")
                .NotNull().WithMessage("Is necessary to inform the Codigo.");

            RuleFor(c => c.PrecoVenda)
                .NotNull().WithMessage("Is necessary to inform the Sell Price.")
                .Must(PrecoVenda => PrecoVenda >= 0).WithMessage("Sell price must be greater or equal to zero.");
                
            RuleFor(c => c.Sku)
                .NotEmpty().WithMessage("Is necessary to inform the Sku.")
                .NotNull().WithMessage("Is necessary to inform the birth Sku.");
        }
			}
}