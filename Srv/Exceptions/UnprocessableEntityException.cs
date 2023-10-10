namespace Api.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException() { }
    public UnprocessableEntityException(string message) : base(message) { }
    public UnprocessableEntityException(string message, Exception inner) : base(message, inner) { }
    protected UnprocessableEntityException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}