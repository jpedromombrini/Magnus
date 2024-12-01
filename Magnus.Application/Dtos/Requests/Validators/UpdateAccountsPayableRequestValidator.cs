using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class UpdateAccountsPayableRequestValidator : AbstractValidator<UpdateAccountsPayableRequest>
{
    public UpdateAccountsPayableRequestValidator()
    {
        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("Informe a data de vencimento");
        RuleFor(x => x.Value)
            .GreaterThan(0).WithMessage("Informe o valor da parcela");
        RuleFor(x => x.CostCenter)
            .NotEmpty().WithMessage("Informe o centro de custo");
        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("Informe a documento");
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Informe o id do usuario");
        RuleFor(x => x.SupplierId)
            .NotEmpty().WithMessage("Informe o id do fornecedor");
    }
}