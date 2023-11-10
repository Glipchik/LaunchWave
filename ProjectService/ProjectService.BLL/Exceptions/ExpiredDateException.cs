namespace ProjectService.BLL.Exceptions;

[Serializable]
public class ExpiredDateException : Exception
{
    public ExpiredDateException() { }

    public ExpiredDateException(string message)
        : base(message) { }

    public ExpiredDateException(string message, Exception inner)
        : base(message, inner) { }

    protected ExpiredDateException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
