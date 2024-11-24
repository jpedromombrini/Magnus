using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class TransferWarehouseItemRequestValidator : AbstractValidator<TransferWarehouseItemRequest>
{
    public TransferWarehouseItemRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do produto deve ter no máximo 100 caracteres.");
        
        RuleFor(x => x.ProductInternalCode)
            .NotEmpty().WithMessage("O código interno do produto é obrigatório.");
        
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que 0.");
        
        RuleFor(x => x.Validity)
            .NotEmpty().WithMessage("A validade é obrigatória.");
    }
}