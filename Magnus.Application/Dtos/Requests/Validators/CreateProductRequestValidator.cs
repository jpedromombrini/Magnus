using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("O código do produto é obrigatório.")
            .Length(1, 10).WithMessage("O código do produto deve ter entre 1 e 10 caracteres.");
     
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .Length(1, 100).WithMessage("O nome do produto deve ter entre 1 e 100 caracteres.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("O preço é obrigatório.")
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.")
            .LessThanOrEqualTo(9999999.99m).WithMessage("O preço deve ser menor ou igual a 9999999,99.");

        RuleFor(x => x.Bars)
            .NotNull().WithMessage("A lista de barras não pode ser nula.");
        RuleFor(x => x.PriceRule)
            .SetValidator(new PriceRuleRequestValidator());
        RuleForEach(x => x.Bars)
            .SetValidator(new BarRequestValidator());
    }
    
}