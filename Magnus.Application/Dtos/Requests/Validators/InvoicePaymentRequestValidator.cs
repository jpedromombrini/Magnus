using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class InvoicePaymentRequestValidator : AbstractValidator<InvoicePaymentRequest>
{
    public InvoicePaymentRequestValidator()
    {
        RuleFor(x => x.PaymentId)
            .NotEmpty().WithMessage("Informe o Pagamento");
        RuleForEach(x => x.Installments)
            .SetValidator(new InvoicePaymentInstallmentRequestValidator());
    }
}