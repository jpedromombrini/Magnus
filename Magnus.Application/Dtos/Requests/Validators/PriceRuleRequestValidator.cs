using System.Security.Cryptography.Xml;
using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class PriceRuleRequestValidator : AbstractValidator<PriceRuleRequest>
{
    public PriceRuleRequestValidator()
    {
        RuleFor(x =>x.From)
            .GreaterThan(0).WithMessage("Infome a quantidade para a partir de");
        RuleFor(x => x.Price)
            .GreaterThan(0m).WithMessage("Informe o preço");
    }
}