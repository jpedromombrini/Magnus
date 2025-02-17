using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class UpdateAppConfigurationRequestValidator : AbstractValidator<UpdateAppConfigurationRequest>
{
    public UpdateAppConfigurationRequestValidator()
    {
        RuleFor(x => x.AmountToDiscount)
            .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero");
    }
}