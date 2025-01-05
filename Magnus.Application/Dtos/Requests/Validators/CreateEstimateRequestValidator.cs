using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateEstimateRequestValidator : AbstractValidator<CreateEstimateRequest>
{
    public CreateEstimateRequestValidator()
    {
        RuleFor(x => x.ValiditAt)
            .NotEmpty()
            .WithMessage("Informe uma data de vencimento")
            .GreaterThan(DateTime.Today.AddDays(-1))
            .WithMessage("Informe a data de vencimento maior que ontem");
        RuleFor(x => x.Freight)
            .GreaterThan(-1)
            .WithMessage("Informe um valor para o frete válido");
        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithMessage("Informe o valor");
        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .WithMessage("Informe a descrição");
        RuleForEach(x => x.Items)
            .SetValidator(new EstimateItemRequestValidator());
    }
    
}