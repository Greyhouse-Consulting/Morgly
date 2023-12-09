using MongoDB.Driver;
using Morgly.Application;
using Morgly.Domain.Events;
using Morgly.Domain.IntegrationEvents;
using Morgly.Domain.Repositories;

namespace Morgly.Infrastructure;

public class EventRepository(IMongoCollection<IntegrationEventContainer> collection, TransactionIdHolder transactionIdHolder)
    : IEventRepository
{
    public void Add<T>(T e) where T : IIntegrationEvent
    {
        var container = new IntegrationEventContainer
        {
            Id = Guid.NewGuid(),
            PayloadType = typeof(T).AssemblyQualifiedName ?? throw new InvalidOperationException(),
            Payload = System.Text.Json.JsonSerializer.Serialize(e),
            TransactionId = transactionIdHolder.TransactionId
        };

        collection.InsertOne(container);
    }

    public async Task Update(IntegrationEventContainer integrationEventHolder)
    {
        //        await _collection.ReplaceOneAsync(f => f.Id == integrationEventHolder.Id, integrationEventHolder);
        await collection.FindOneAndReplaceAsync(f => f.Id == integrationEventHolder.Id, integrationEventHolder);
    }

    public async Task MarkAsSent(Guid id)
    {
        await collection.UpdateOneAsync(f => f.Id == id, Builders<IntegrationEventContainer>.Update.Set(f => f.Status, Status.Sent));
    }

    public async Task<IntegrationEventContainer> Get(Guid eId)
    {
        return await collection.Find(f => f.Id == eId).FirstOrDefaultAsync();
    }

    public async Task<ICollection<IntegrationEventContainer>> GetUnpublished(Guid transactionId)
    {
        return await collection.Find(f => f.Status == Status.New & f.TransactionId == transactionId).ToListAsync();
    }
}