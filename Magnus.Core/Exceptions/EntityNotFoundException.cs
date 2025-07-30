namespace Magnus.Core.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message)
        : base(message)
    {
    }

    public EntityNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public EntityNotFoundException(Guid id)
        : base($"O Item com ID {id} não foi encontrado no banco de dados")
    {
        Id = id;
    }

    public Guid Id { get; }
}