using System.Text.RegularExpressions;
using FluentValidation;
using Magnus.Core.Enumerators;

namespace Magnus.Application.Dtos.Requests.Validators;

public class ClientPhoneRequestValidator : AbstractValidator<ClientPhoneRequest>
{
    public ClientPhoneRequestValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("O número de telefone não pode ser vazio.")
            .Must((number) => PhoneValidator.IsValidPhoneNumber(number))
            .WithMessage("Número de telefone inválido para o tipo selecionado.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Informe a descrição")
            .MaximumLength(50).WithMessage("A descrição deve ter no máximo 50 caracteres");
    }
   
}