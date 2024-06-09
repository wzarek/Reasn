namespace ReasnAPI.Services.Exceptions;
public class ObjectExistsException : Exception
{
    public ObjectExistsException() : base() { }

    public ObjectExistsException(string message) : base(message) { }

    public ObjectExistsException(string message, Exception innerException) : base(message, innerException) { }
}
