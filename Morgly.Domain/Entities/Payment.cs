namespace Morgly.Domain.Entities;

// Create class payment with amount and date and due date
public class Payment(decimal amount, DateTime date, DateOnly dueDate)
{
    public decimal Amount { get; } = amount;
    public DateTime Date { get; } = date;
    public DateOnly DueDate { get; } = dueDate;
}