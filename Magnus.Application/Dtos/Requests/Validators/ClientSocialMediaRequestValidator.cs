using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class ClientSocialMediaRequestValidator : AbstractValidator<ClientSocialMediaRequest>
{
    public ClientSocialMediaRequestValidator()
    {
        RuleFor(x => x.Link)
            .NotEmpty().WithMessage("informe o link")
            .MaximumLength(150).WithMessage("o link deve ter no máximo 150 caracteres");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("informe o nome")
            .MaximumLength(100).WithMessage("o nome deve ter no máximo 100 caracteres");
        
    }
}