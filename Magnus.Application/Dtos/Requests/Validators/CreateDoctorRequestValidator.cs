using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequest>
{
    public CreateDoctorRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Informe o nome.")
            .MaximumLength(100).WithMessage("O nome dee ter no máximo 100 caracteres.");
        RuleFor(x => x.Crm)
            .NotEmpty().WithMessage("Informe a crm.")
            .MaximumLength(20).WithMessage("O Crm deve ter no máximo 20 caracteres.");
    }
}