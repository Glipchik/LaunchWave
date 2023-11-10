namespace ProjectService.BLL.Exceptions;

public class InvalidStatusException : Exception
{
    public InvalidStatusException() { }

    public InvalidStatusException(string message)
        : base(message) { }

    public InvalidStatusException(string message, Exception inner)
        : base(message, inner) { }

    protected InvalidStatusException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
