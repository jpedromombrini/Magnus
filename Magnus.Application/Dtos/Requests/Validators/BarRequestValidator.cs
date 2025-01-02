using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class BarRequestValidator : AbstractValidator<BarRequest>
{
    public BarRequestValidator()
    {
        RuleFor(x =>x.Code)
            .NotEmpty().WithMessage("Informe um código de barra")
            .Length(1,14).WithMessage("Código de barras deve ter no máximo 14 caracteres");
    }
}