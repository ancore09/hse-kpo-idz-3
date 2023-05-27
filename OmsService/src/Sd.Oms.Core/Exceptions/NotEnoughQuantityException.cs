using System.Runtime.Serialization;

namespace Sd.Oms.Core.Exceptions;

public class NotEnoughQuantityException: DomainException
{
    public NotEnoughQuantityException()
    {
    }

    protected NotEnoughQuantityException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotEnoughQuantityException(string? message) : base(message)
    {
    }

    public NotEnoughQuantityException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}