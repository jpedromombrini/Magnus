using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class RefreshLoginRequestValidator : AbstractValidator<RefreshLoginRequest>
{
    public RefreshLoginRequestValidator()
    {
        RuleFor(x =>x.RefreshToken)
            .NotEmpty().WithMessage("Informe o refresh token");
            
    } 
}