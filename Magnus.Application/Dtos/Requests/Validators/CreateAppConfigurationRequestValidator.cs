using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateAppConfigurationRequestValidator : AbstractValidator<CreateAppConfigurationRequest>
{
    public CreateAppConfigurationRequestValidator()
    {
        RuleFor(x => x.AmountToDiscount)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero");
    }
}