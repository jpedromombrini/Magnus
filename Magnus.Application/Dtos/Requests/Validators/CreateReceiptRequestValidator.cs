using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateReceiptRequestValidator : AbstractValidator<CreateReceiptRequest>
{
    public CreateReceiptRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Informe um nome")
            .Length(1,100).WithMessage("Informe um nome com no máximo 100 caracteres");
        RuleFor(x => x.Increase)
            .GreaterThan(-1).WithMessage("Informe o acrescimo");
    }
}