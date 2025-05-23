using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class InvoiceItemRequestValidator : AbstractValidator<InvoiceItemRequest>
{
    public InvoiceItemRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .Length(1, 100).WithMessage("O nome do produto deve ter entre 1 e 100 caracteres.");

        RuleFor(x => x.TotalValue)
            .GreaterThan(0).WithMessage("O valor total deve ser maior que 0.")
            .ScalePrecision(2, 10).WithMessage("O valor total deve ter até 2 casas decimais e até 10 dígitos.");
    }
}