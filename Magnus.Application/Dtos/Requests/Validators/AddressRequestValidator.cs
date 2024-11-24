using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class AddressRequestValidator : AbstractValidator<AddressRequest>
{
    public AddressRequestValidator()
    {
        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("O CEP é obrigatório.")
            .Matches(@"^\d{5}-\d{3}$").WithMessage("O CEP deve estar no formato 'XXXXX-XXX'.");
        
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