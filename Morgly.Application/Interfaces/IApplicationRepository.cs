using Morgly.Domain.Entities;

namespace Morgly.Application.Interfaces
{
    public interface IApplicationRepository : IRepository<MortgageApplication>
    {
        Task Add(MortgageApplication application);
        Task UpdateStatus(Guid messageApplicationId, string messageStatus);
        Task Update(MortgageApplication application);
    }
}
