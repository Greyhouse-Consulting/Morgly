using System.Reflection;
using System.Text.Json;

namespace Morgly.Domain.Events;

//public class IntegrationEventHolder
//{
//    public Guid Id { get; set; }
//    public string EventPayload { get; private set; }

//    public Status Status { get; private set; }
//    public IntegrationEventHolder(object @event)
//    {
//        EventType = @event.GetType().AssemblyQualifiedName.ToString();
//        EventPayload = JsonSerializer.Serialize(@event);
//        Status = Status.New;
//    }


//    public void MarkAsSent()
//    {
//        Status = Status.Sent;
//    }
//    public string EventType { get; set; }
//}

public enum Status
{
    New,
    Sent
}