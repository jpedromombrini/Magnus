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
            .Must((number) => PhoneValidator.IsValidPhoneNumber(number))
            .WithMessage("Número de telefone inválido para o tipo selecionado.");
        
        RuleFor(seller => seller.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail fornecido não tem um formato válido.")
            .Length(1, 100).WithMessage("O e-mail deve ter entre 1 e 100 caracteres.");
        
        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("O CEP é obrigatório.")
            .Matches(@"^\d{8}-\d{3}$").WithMessage("O CEP deve estar no formato 'XXXXX-XXX'.");
        
        RuleFor(x => x.PublicPlace)
            .NotEmpty().WithMessage("O logradouro é obrigatório.")
            .Length(3, 100).WithMessage("O logradouro deve ter entre 3 e 100 caracteres.");
        
        RuleFor(x => x.Number)
            .GreaterThan(-1).WithMessage("O número da residência deve ser positivo.");
        
        RuleFor(x => x.Neighborhood)
            .NotEmpty().WithMessage("O bairro é obrigatório.")
            .Length(3, 50).WithMessage("O bairro deve ter entre 3 e 50 caracteres.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("A cidade é obrigatória.")
            .Length(3, 50).WithMessage("A cidade deve ter entre 3 e 50 caracteres.");
        
        RuleFor(x => x.State)
            .NotEmpty().WithMessage("O estado é obrigatório.")
            .Length(2, 2).WithMessage("O estado deve ter 2 caracteres.");

        RuleFor(x => x.Complement)
            .Length(0, 50).WithMessage("O complemento deve ter entre 0 e 50 caracteres.");
    }
}