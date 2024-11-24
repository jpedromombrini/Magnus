using System.Text.RegularExpressions;
using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
{
    public CreateClientRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
        
        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("O documento é obrigatório.")
            .Must(DocumentValidator.IsValidDocument).WithMessage("O documento fornecido deve ser um CPF ou CNPJ válido.");
        
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("O e-mail fornecido não é válido.")
            .When(x => !string.IsNullOrEmpty(x.Email)); 
        
        RuleFor(x => x.Occupation)
            .Length(3, 50).WithMessage("A ocupação deve ter entre 3 e 50 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Occupation)); 
        
        RuleFor(x => x.Address)
            .SetValidator(new AddressRequestValidator()) 
            .When(x => x.Address != null);
        
        RuleFor(x => x.RegisterNumber)
            .Length(3, 50).WithMessage("O número de registro deve ter entre 3 e 50 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.RegisterNumber)); 
        
        RuleForEach(x => x.SocialMedias)
            .SetValidator(new ClientSocialMediaRequestValidator())
            .When(x => x.SocialMedias != null && x.SocialMedias.Any());
        
        RuleForEach(x => x.Phones)
            .SetValidator(new ClientPhoneRequestValidator())
            .When(x => x.Phones != null && x.Phones.Any()); 
    }
}