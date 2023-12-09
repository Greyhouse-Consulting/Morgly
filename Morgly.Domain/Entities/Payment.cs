namespace Morgly.Domain.Entities;

// Create class payment with amount and date and due date
public class Payment
{
    public decimal Amount { get; }
    public DateTime Date { get; }
    public DateTime DueDate { get; }

    public Payment(decimal amount, DateTime date, DateTime dueDate)
    {
        Amount = amount;
        Date = date;
        DueDate = dueDate;
    }
}