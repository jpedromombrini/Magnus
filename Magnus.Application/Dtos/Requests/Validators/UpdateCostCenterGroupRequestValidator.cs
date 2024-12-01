using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class UpdateCostCenterGroupRequestValidator : AbstractValidator<UpdateCostCenterGroupRequest>
{
    public UpdateCostCenterGroupRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
    }
}