using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("Formato de e-mail inválido.")
            .Length(5, 100).WithMessage("O e-mail deve ter entre 5 e 100 caracteres.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

        RuleFor(x => x.InitialDate)
            .NotEmpty().WithMessage("A data inicial é obrigatória.")
            .LessThan(x => x.FinalDate).WithMessage("A data inicial deve ser anterior à data final.");

        RuleFor(x => x.FinalDate)
            .NotEmpty().WithMessage("A data final é obrigatória.")
            .GreaterThan(x => x.InitialDate).WithMessage("A data final deve ser posterior à data inicial.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .Length(3, 40).WithMessage("A senha deve ter entre 8 e 40 caracteres.");
    }
}