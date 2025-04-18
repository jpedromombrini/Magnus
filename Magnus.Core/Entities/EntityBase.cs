namespace Magnus.Core.Entities;

public abstract class EntityBase
{
    public Guid Id { get; private set; } = Guid.NewGuid();
}