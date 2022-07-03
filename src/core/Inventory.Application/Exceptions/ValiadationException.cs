namespace Inventory.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : this("Validation error occured")
    {
    }

    public ValidationException(String message) : base(message)
    {
    }

    public ValidationException(Exception exception) : this(exception.Message)
    {
    }
}