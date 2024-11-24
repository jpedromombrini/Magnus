using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateLaboratoryRequestValidator : AbstractValidator<CreateLaboratoryRequest>
{
    public CreateLaboratoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Informe um nome")
            .Length(1,100).WithMessage("Informe um nome com no máximo 100 caracteres");
        
    }
}