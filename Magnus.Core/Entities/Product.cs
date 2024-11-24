namespace Magnus.Core.Entities;

public class Product : EntityBase
{
    public int InternalCode { get; private set; }
    public string Code { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public PriceRule? PriceRule { get; private set; }
    public List<Bar>? Bars { get; private set; }
    public Guid LaboratoryId { get; private set; }

    private Product()
    {
    }

    public Product(string code, string name, decimal price, PriceRule? priceRule, List<Bar>? bars, Guid laboratoryId)
    {
        SetCode(code);
        SetName(name);
        SetPrice(price);
        SetPriceRule(priceRule);
        SetBars(bars);
        SetLaboratoryId(laboratoryId);
    }

    public void SetCode(string code)
    {
        if (string.IsNullOrEmpty(code))
        {
            throw new ArgumentNullException(nameof(code), "O código não pode ser nulo ou vazio.");
        }

        Code = code;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "O nome não pode ser nulo ou vazio.");
        Name = name;
    }

    public void SetPrice(decimal price)
    {
        if (price < 0)
        {
            throw new ArgumentException("O preço não pode ser negativo.");
        }

        Price = price;
    }

    public void SetPriceRule(PriceRule? priceRule)
    {
        PriceRule = priceRule;
    }

    public void SetBars(List<Bar>? bars)
    {
        Bars = bars ?? [];
    }

    public void SetLaboratoryId(Guid laboratoryId)
    {
        if (laboratoryId == Guid.Empty)
        {
            throw new ArgumentException("O LaboratoryId não pode ser um Guid vazio.");
        }

        LaboratoryId = laboratoryId;
    }
}