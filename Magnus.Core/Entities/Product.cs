namespace Magnus.Core.Entities;

public class Product : EntityBase
{
    private Product()
    {
    }

    public Product(string name, decimal price, Guid laboratoryId, bool applyPriceRule)
    {
        SetName(name);
        SetPrice(price);
        SetLaboratoryId(laboratoryId);
        SetApplyPriceRule(applyPriceRule);
    }

    public int InternalCode { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public ICollection<Bar>? Bars { get; private set; }
    public Guid LaboratoryId { get; private set; }
    public bool ApplyPriceRule { get; private set; }
    public ICollection<ProductPriceTable>? ProductPriceTables { get; private set; }

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

    public void AddBars(IEnumerable<Bar> bars)
    {
        Bars ??= [];
        Bars = bars.ToList();
    }

    public void SetLaboratoryId(Guid laboratoryId)
    {
        if (laboratoryId == Guid.Empty) throw new ArgumentException("O LaboratoryId não pode ser um Guid vazio.");

        LaboratoryId = laboratoryId;
    }

    public void AddProductPriceTable(ProductPriceTable priceTable)
    {
        ProductPriceTables ??= [];
        ProductPriceTables.Add(priceTable);
    }

    public void AddProductPriceTables(IEnumerable<ProductPriceTable> priceTables)
    {
        ProductPriceTables ??= [];
        ProductPriceTables = priceTables.ToList();
    }

    public void RemoveProductPriceTable(ProductPriceTable priceTable)
    {
        if (ProductPriceTables == null || ProductPriceTables.Count == 0)
            return;
        ProductPriceTables.Remove(priceTable);
    }

    public void SetApplyPriceRule(bool applyPriceRule)
    {
        ApplyPriceRule = applyPriceRule;
    }
}