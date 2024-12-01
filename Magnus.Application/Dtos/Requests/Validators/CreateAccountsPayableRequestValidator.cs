using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateAccountsPayableRequestValidator : AbstractValidator<CreateAccountsPayableRequest>
{
    public CreateAccountsPayableRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Informe o id do usuario");
        RuleFor(x => x.SupplierId)
            .NotEmpty().WithMessage("Informe o id do fornecedor");
        RuleFor(x => x.CostCenterCode)
            .NotEmpty().WithMessage("Informe o centro de custo");
        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("Informe a documento");
        RuleForEach(x => x.AccountsPayableRequests)
            .SetValidator(new AccountsPayableRequestValidator());
    } 
}