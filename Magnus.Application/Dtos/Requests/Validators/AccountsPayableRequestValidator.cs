using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class AccountsPayableRequestValidator : AbstractValidator<AccountsPayableRequest>
{
    public AccountsPayableRequestValidator()
    {
        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("Informe a data de vencimento");
        RuleFor(x => x.Installment)
            .GreaterThan(0).WithMessage("Informe o número da parcela");
        RuleFor(x => x.Value)
            .GreaterThan(0).WithMessage("Informe o valor da parcela");
    }
}