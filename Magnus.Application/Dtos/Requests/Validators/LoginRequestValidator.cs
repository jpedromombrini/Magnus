using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Informe o usuário")
            .EmailAddress().WithMessage("Email inválido");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Informe a senha");
    }   
}