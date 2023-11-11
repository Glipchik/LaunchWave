namespace ProjectService.BLL.Exceptions;

[Serializable]
public class ModelNotFoundException : NullReferenceException
{
    public ModelNotFoundException() { }

    public ModelNotFoundException(string message)
        : base(message) { }

    public ModelNotFoundException(string message, Exception inner)
        : base(message, inner) { }

    protected ModelNotFoundException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
