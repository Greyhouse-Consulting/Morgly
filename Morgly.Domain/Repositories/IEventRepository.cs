using Morgly.Domain.IntegrationEvents;

namespace Morgly.Domain.Repositories;

public interface IEventRepository
{
    Task<ICollection<IntegrationEventContainer>> GetUnpublished(Guid transactionId);
    void Add<T>(T e) where T : IIntegrationEvent;
        
    Task Update(IntegrationEventContainer integrationEventHolder);
    Task MarkAsSent(Guid id);
    Task<IntegrationEventContainer> Get(Guid eId);
}