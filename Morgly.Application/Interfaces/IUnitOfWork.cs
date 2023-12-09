namespace Morgly.Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellationToken);
    IMortgageRepository Mortgages { get; }
    IMongoDbTransaction BeginTransaction();
}

public interface IMongoDbTransaction
{
    public void Commit();
    public void Rollback();
}