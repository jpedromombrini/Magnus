namespace Magnus.Core.Exceptions;

public class ProductWithoutStockException()
    : BusinessRuleException("O Item não tem estoque no depósito de origem")
{
}