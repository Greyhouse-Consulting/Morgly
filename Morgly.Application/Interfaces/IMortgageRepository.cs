namespace Morgly.Application.Interfaces;

public interface IMortgageRepository : IRepository<Domain.Entities.Mortgage>
{
    public Task Add(Domain.Entities.Mortgage mortgage);
    
    
}