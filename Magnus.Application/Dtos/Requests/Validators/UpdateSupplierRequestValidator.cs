using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class UpdateSupplierRequestValidator : AbstractValidator<UpdateSupplierRequest>
{
    public UpdateSupplierRequestValidator()
    {
        RuleFor(seller => seller.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");
        
        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("O documento é obrigatório.")
            .Must(DocumentValidator.IsValidDocument).WithMessage("O documento fornecido deve ser um CPF ou CNPJ válido.");
        
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("O número de telefone não pode ser vazio.")
            .Must((request, number) => PhoneValidator.IsValidPhoneNumber(number, request.PhoneType))
            .WithMessage("Número de telefone inválido para o tipo selecionado.");
        
        RuleFor(seller => seller.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail fornecido não tem um formato válido.")
            .Length(1, 100).WithMessage("O e-mail deve ter entre 1 e 100 caracteres.");
        
        RuleFor(x => x.Address)
            .SetValidator(new AddressRequestValidator());
    }
}