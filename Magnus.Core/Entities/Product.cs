namespace Magnus.Core.Entities;

public class Product : EntityBase
{
    public int InternalCode { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public decimal Discount { get; private set; }
    public List<Bar>? Bars { get; private set; }
    public Guid LaboratoryId { get; private set; }

    private Product()
    {
    }

    public Product(string name, decimal price, List<Bar>? bars, Guid laboratoryId)
    {
        SetName(name);
        SetPrice(price);
        SetBars(bars);
        SetLaboratoryId(laboratoryId);
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
            throw new ArgumentException("O preço não pode ser negativo.");
        Price = price;
    }

    public void SetBars(List<Bar>? bars)
    {
        Bars = bars ?? [];
    }

    public void AddBar(Bar bar)
    {
        Bars ??= [];
        Bars.Add(bar);
    }

    public void SetLaboratoryId(Guid laboratoryId)
    {
        if (laboratoryId == Guid.Empty)
        {
            throw new ArgumentException("O LaboratoryId não pode ser um Guid vazio.");
        }

        LaboratoryId = laboratoryId;
    }

    public void SetDiscount(decimal discount)
    {
        if(discount <0)
            throw new ArgumentException("O desconto não pode ser negativo.");
        Discount = discount;
    }
}