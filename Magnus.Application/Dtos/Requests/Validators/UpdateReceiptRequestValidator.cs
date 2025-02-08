using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class UpdateReceiptRequestValidator : AbstractValidator<UpdateReceiptRequest>
{
    public UpdateReceiptRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Informe um nome")
            .Length(1,100).WithMessage("Informe um nome com no máximo 100 caracteres");
        RuleFor(x => x.Increase)
            .NotEmpty().WithMessage("Informe o acrescimo");
    }
}