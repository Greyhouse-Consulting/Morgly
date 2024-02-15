using Morgly.Domain.Entities;

namespace Morgly.Application.Interfaces;

public interface IMortgageRepository : IRepository<Domain.Entities.Mortgage>
{
    public Task Add(Domain.Entities.Mortgage mortgage);
    Task Update(Mortgage mortgage);
    Task Delete(Guid id);
    Task<IEnumerable<Term>> GetTerms(Guid mortgageId);
}