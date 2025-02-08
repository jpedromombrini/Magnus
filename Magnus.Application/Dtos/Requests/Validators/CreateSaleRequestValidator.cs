using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleForEach(x =>x.Receipts)
            .SetValidator(new SaleReceiptRequestValidator())
            .When(x => x.Receipts != null && x.Receipts.Any()) ;
        RuleForEach(x => x.Items)
            .SetValidator(new SaleItemRequestValidator());
        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Informe um valor");
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage("Informe o cliente");
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("Informe o usuário");
    }
}