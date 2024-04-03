namespace Morgly.Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChanges();
    IMortgageRepository Mortgages { get; }
    CancellationTokenSource Cts { get; }


    Task BeginTransaction();
    Task AbortTransaction();
}

public interface IMongoDbTransaction
{
    public void Commit();
    public void Rollback();
}