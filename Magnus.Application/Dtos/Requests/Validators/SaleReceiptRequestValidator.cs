using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class SaleReceiptRequestValidator : AbstractValidator<CreateSaleReceiptRequest>
{
    public SaleReceiptRequestValidator()
    {
        RuleFor(x => x.ReceiptId)
            .NotEmpty()
            .WithMessage("Informe uma forma de recebimento");
        RuleForEach(x => x.Installments)
            .SetValidator(new SaleReceiptInstallmentRequestValidator());
    }
}