using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateSellerRequestValidator : AbstractValidator<CreateSellerRequest>
{
    public CreateSellerRequestValidator()
    {
        RuleFor(seller => seller.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");
        
        RuleFor(seller => seller.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail fornecido não tem um formato válido.")
            .Length(1, 100).WithMessage("O e-mail deve ter entre 1 e 100 caracteres.");
        
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("O número de telefone não pode ser vazio.")
            .Must((number) => PhoneValidator.IsValidPhoneNumber(number))
            .WithMessage("Número de telefone inválido para o tipo selecionado.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .Length(3, 40).WithMessage("A senha deve ter entre 8 e 40 caracteres.");
    }
}