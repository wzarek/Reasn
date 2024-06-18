namespace ReasnAPI.Exceptions;

public class VerificationException : Exception
{
    public VerificationException() { }

    public VerificationException(string message) : base(message) { }

    public VerificationException(string message, Exception inner) : base(message, inner) { }
}