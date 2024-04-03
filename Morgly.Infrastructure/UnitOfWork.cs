using MassTransit.MongoDbIntegration;
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
    private readonly MongoDbContext _context;
    public CancellationTokenSource Cts { get; private set; }

    public IClientSessionHandle? Session { get; set; }

    public UnitOfWork(IMortgageRepository mortgageRepository, IMongoClient client, MongoDbContext context )
    {
        _client = client;
        _context = context;
        Mortgages = mortgageRepository;
        Session = context.Session;
    }

    public async Task SaveChanges()
    {
        await _context.CommitTransaction(Cts.Token);
        Cts.Dispose();
    }


    public IMortgageRepository Mortgages { get; }


    public async Task BeginTransaction()
    {
        Cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        await _context.BeginTransaction(Cts.Token);
    }

    public async Task AbortTransaction()
    {
        await _context.AbortTransaction(Cts.Token);
        Cts.Dispose();
    }
    // Add mortgages repository property here
}