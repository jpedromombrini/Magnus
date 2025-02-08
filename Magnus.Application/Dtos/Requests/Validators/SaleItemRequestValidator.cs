using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class SaleItemRequestValidator : AbstractValidator<SaleItemRequest>
{
    public SaleItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Informe o ID da produto");
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("Informe o nome da produto");
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Informe a quantidade");
        RuleFor(x => x.Discount)
            .GreaterThan(-1)
            .WithMessage("Informe um desconto válido");
        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Informe um valor vállido");
        RuleFor(x => x.TotalPrice)
            .GreaterThan(0)
            .WithMessage("Informe um valor total");
        RuleFor(x => x.Validity)
            .NotEmpty()
            .WithMessage("Informe a validade");
    }
}