namespace Morgly.Application;

public class TransactionIdHolder
{
    public readonly Guid TransactionId = Guid.NewGuid();
}