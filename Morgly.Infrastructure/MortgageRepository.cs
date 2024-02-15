using MongoDB.Driver;
using Morgly.Application.Interfaces;
using Morgly.Domain.Entities;

namespace Morgly.Infrastructure;

public class MortgageRepository(IMongoCollection<Mortgage> coll) : IMortgageRepository
{
    public Task<Mortgage> Get(Guid id)
    {
        var startDate = DateTime.Now;

        return Task.FromResult(new Mortgage(Guid.NewGuid(), 200000m, new Term(new TermDate(startDate.Year, startDate.Month), 12, 3)));
    }

    public async  Task<IEnumerable<Mortgage>> GetAll()
    {
        return await coll.Find(_ => true).ToListAsync();
    }

    public async Task Add(Mortgage mortgage)
    {
        await coll.InsertOneAsync(mortgage);
    }

    public async Task Update(Mortgage mortgage)
    {
        await coll.ReplaceOneAsync(m => m.Id == mortgage.Id, mortgage);
    }

    public async Task Delete(Guid id)
    {
        await coll.DeleteOneAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Term>> GetTerms(Guid mortgageId)
    {
        var mortgage = await coll.Find(m => m.Id == mortgageId).FirstOrDefaultAsync();

        return mortgage.GetTerms();
    }

}

