using MassTransit.MongoDbIntegration;
using MongoDB.Driver;
using Morgly.Application.Interfaces;
using Morgly.Domain.Entities;

namespace Morgly.Infrastructure;

public class ApplicationRepository(IMongoCollection<MortgageApplication> coll, MongoDbContext context, IUnitOfWork unitOfWork) : IApplicationRepository
{
    public async  Task<MortgageApplication> Get(Guid id)
    {
        return await coll.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<MortgageApplication>> GetAll()
    {
        return await coll.Find(_ => true).ToListAsync();
    }

    public async Task Add(MortgageApplication application)
    {
        await coll.InsertOneAsync(context.Session,application, null, unitOfWork.Cts.Token);
    }

    public async Task UpdateStatus(Guid messageApplicationId, string messageStatus)
    {
        await coll.UpdateOneAsync(x => x.Id == messageApplicationId, Builders<MortgageApplication>.Update.Set(x => x.Status, messageStatus));
    }

    public async Task Update(MortgageApplication application)
    {
        await coll.ReplaceOneAsync(context.Session, x => x.Id == application.Id, application, (ReplaceOptions)null , unitOfWork.Cts.Token);
    }
}