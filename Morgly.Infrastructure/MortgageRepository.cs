using MongoDB.Driver;
using Morgly.Application.Interfaces;
using Morgly.Domain.Entities;

namespace Morgly.Infrastructure;

public class MortgageRepository(IMongoCollection<Mortgage> coll) : IMortgageRepository
{
    public Task<Mortgage> Get(Guid id)
    {
        return Task.FromResult(new Mortgage(Guid.NewGuid(), 200000m, new Term(DateTime.Now, 12, 3)));
    }

    public async  Task<IEnumerable<Mortgage>> GetAll()
    {
        return await coll.Find(_ => true).ToListAsync();
    }

    public async Task Add(Mortgage mortgage)
    {
        await coll.InsertOneAsync(mortgage);
    }
}