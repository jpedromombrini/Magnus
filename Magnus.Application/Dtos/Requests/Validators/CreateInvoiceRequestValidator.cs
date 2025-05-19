using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateInvoiceRequestValidator : AbstractValidator<CreateInvoiceRequest>
{
    public CreateInvoiceRequestValidator()
    {
        RuleFor(invoice => invoice.Number)
            .GreaterThan(0)
            .WithMessage("Número da Nota deve ser maior que zero.");
     
        RuleFor(invoice => invoice.Serie)
            .GreaterThan(0)
            .WithMessage("Série deve ser maior que zero.");

        RuleFor(invoice => invoice.Date)
            .NotEmpty()
            .WithMessage("A data da nota não pode ser vazia.");

        RuleFor(invoice => invoice.DateEntry)
            .NotEmpty()
            .WithMessage("A data de entrada não pode ser vazia.");

        RuleFor(invoice => invoice.Key)
            .MaximumLength(44)
            .WithMessage("A chave não pode ter mais que 44 caracteres.");

        RuleFor(invoice => invoice.Deduction)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Deduções não podem ser negativas.");

        RuleFor(invoice => invoice.Freight)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O valor do frete não pode ser negativo.");

        RuleFor(invoice => invoice.Value)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O valor da fatura não pode ser negativo.");

        RuleFor(invoice => invoice.Observation)
            .MaximumLength(200)
            .WithMessage("A observação não pode ter mais que 200 caracteres.");

        RuleFor(invoice => invoice.SupplierName)
            .MaximumLength(100)
            .WithMessage("O nome do fornecedor não pode ter mais que 100 caracteres.");

        RuleForEach(invoice => invoice.Items)
            .NotEmpty()
            .WithMessage("A fatura deve ter pelo menos um item.")
            .SetValidator(new InvoiceItemRequestValidator());
    }
}