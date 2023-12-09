namespace Morgly.Domain.Entities;

public class MortgageApplication : Entity
{
    public Guid Id { get; set; }
    public string Status { get; set; } = "new";
    public decimal Amount { get; set; }
}