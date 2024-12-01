using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateCostCenterGroupRequestValidator : AbstractValidator<CreateCostCenterGroupRequest>
{
    public CreateCostCenterGroupRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
        RuleFor(x=> x.Code)
            .NotEmpty().WithMessage("O código é obrigatório")
            .Length(2).WithMessage("O código deve conter 2 caracteres.");
    }
}