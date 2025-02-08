using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class SaleReceiptInstallmentRequestValidator : AbstractValidator<SaleReceiptInstallmentRequest>
{
    public SaleReceiptInstallmentRequestValidator()
    {
        RuleFor(x => x.DueDate)
            .NotEmpty()
            .WithMessage("Informe uma data de vencimento");
        RuleFor(x => x.Value)
            .NotEmpty()
            .WithMessage("Informe um valor");  
        
        RuleFor(x => x.Installment)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Informe uma parcela válida");
    }
}