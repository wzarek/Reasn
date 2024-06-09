namespace ReasnAPI.Exceptions;

public class ObjectInUseException : Exception
{
    public ObjectInUseException() { }

    public ObjectInUseException(string message) : base(message) { }

    public ObjectInUseException(string message, Exception innerException) : base(message, innerException) { }
}