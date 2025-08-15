namespace Magnus.Core.Entities;

public class ProductGroup : EntityBase
{
    private ProductGroup()
    {
    }

    public ProductGroup(string name)
    {
        SetName(name);
    }

    public string Name { get; private set; }
    public ICollection<Product> Products { get; set; }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (name.Length > 100)
            throw new ArgumentOutOfRangeException(nameof(name));
        Name = name;
    }
}