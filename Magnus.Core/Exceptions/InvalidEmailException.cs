namespace Magnus.Core.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException(string message) 
        : base(message) { }
    
    public InvalidEmailException(string message, Exception innerException)
        : base(message, innerException) { }
}