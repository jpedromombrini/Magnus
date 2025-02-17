namespace Magnus.Core.Entities;

public class AppConfiguration : EntityBase
{
    public int AmountToDiscount { get; private set; }

    private AppConfiguration(){}

    public AppConfiguration(int amountToDiscount)
    {
        SetAmountToDiscount(amountToDiscount);
    }

    public void SetAmountToDiscount(int amountToDiscount)
    {
        if(amountToDiscount < 0)
            throw new ArgumentException("Quantidade para desconto não pode ser negativo.");
        AmountToDiscount = amountToDiscount;
    }
}