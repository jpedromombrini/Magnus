namespace Magnus.Core.Exceptions;

public class ProductWithoutStockException()
    : Exception($"O Item não tem estoque no depósito de origem")
{
}