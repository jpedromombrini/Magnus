using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class EstimateItemRequestValidator : AbstractValidator<EstimateItemRequest>
{
    public EstimateItemRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Informe a quantidade");
        RuleFor(x => x.Discount)
            .GreaterThan(-1)
            .WithMessage("Informe um desconto válido");
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Informe o Id do Produto");
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("Informe o Nome do Produto");
        RuleFor(x => x.TotalValue)
            .GreaterThan(0)
            .WithMessage("Informe o valor total do produto");
    }
}
