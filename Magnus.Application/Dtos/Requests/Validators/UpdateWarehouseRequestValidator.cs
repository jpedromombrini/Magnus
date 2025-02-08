using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class UpdateWarehouseRequestValidator : AbstractValidator<UpdateWarehouseRequest>
{
    public UpdateWarehouseRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Informe o nome.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Informe o usuário.");
    }
}