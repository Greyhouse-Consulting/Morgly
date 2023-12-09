using MongoDB.Driver;
using Morgly.Application.Interfaces;

namespace Morgly.Infrastructure;


public class MongoDbTransaction : IMongoDbTransaction
{
    private readonly IClientSessionHandle _session;

    public MongoDbTransaction(IClientSessionHandle session)
    {
        _session = session;
        _session.StartTransaction();
    }

    public void Commit()
    {
        _session.CommitTransaction();
    }

    public void Rollback()
    {
        _session.AbortTransaction();
    }
}



public class UnitOfWork : IUnitOfWork
{
    private readonly IMongoClient _client;

    public UnitOfWork(IMortgageRepository mortgageRepository, IMongoClient client)
    {
        _client = client;
        Mortgages = mortgageRepository;
    }

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public IMortgageRepository Mortgages { get; }


    public IMongoDbTransaction BeginTransaction()
    {

        return new MongoDbTransaction(_client.StartSession());
    }
    // Add mortgages repository property here
}