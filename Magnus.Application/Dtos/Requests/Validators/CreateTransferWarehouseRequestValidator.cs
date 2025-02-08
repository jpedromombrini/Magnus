using FluentValidation;

namespace Magnus.Application.Dtos.Requests.Validators;

public class CreateTransferWarehouseRequestValidator : AbstractValidator<CreateTransferWarehouseRequest>
{
    public CreateTransferWarehouseRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("O ID do usuário é obrigatório.");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("O nome do usuário é obrigatório.").MaximumLength(100);
        RuleFor(x => x.WarehouseDestinyId)
            .GreaterThan(-1).WithMessage("O ID do depósito de destino é obrigatório.");
        RuleFor(x => x.WarehouseDestinyName).NotEmpty().WithMessage("O nome do depósito de destino é obrigatório.")
            .MaximumLength(100);
        RuleFor(x => x.WarehouseOriginId)
            .GreaterThan(-1)
            .WithMessage("O ID do depósito de origem é obrigatório.");
        RuleFor(x => x.WarehouseOriginName).NotEmpty().WithMessage("O nome do depósito de origem é obrigatório.")
            .MaximumLength(100);
        RuleForEach(x => x.Items)
            .SetValidator(new TransferWarehouseItemRequestValidator());
    }
}