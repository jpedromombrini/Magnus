using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class InvoicePaymentInstallmentRequestValidator : AbstractValidator<InvoicePaymentInstallmentRequest>
{
    public InvoicePaymentInstallmentRequestValidator()
    {
        RuleFor(x => x.InvoicePaymentId)
            .NotEmpty().WithMessage("Informe o Id da Nota");
        RuleFor(x => x.Value)
            .GreaterThan(0).WithMessage("Informe a Valor");
        RuleFor(x => x.Installment)
            .GreaterThan(0).WithMessage("Informe a parcela");
    }
}