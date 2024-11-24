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
            .NotEmpty().WithMessage("Informe o acrescimo");
        RuleFor(x => x.InInstallments)
            .NotEmpty().WithMessage("Informe se é Parcelado");
    }
}