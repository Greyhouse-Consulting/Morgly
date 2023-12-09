namespace Morgly.Domain.Entities;

// create public class Term with startdate and number of months as properties   
public class Term
{
    public DateTime StartDate { get; }
    public int NumberOfMonths { get; }
    public decimal InterestRate { get; }

    public Term(DateTime startDate, int numberOfMonths, decimal interestRate)
    {
        StartDate = startDate;
        NumberOfMonths = numberOfMonths;
        InterestRate = interestRate;
    }

    // Given start date create function that return the end date of the term using number of months 
    public DateTime GetEndDate()
    {
        return StartDate.AddMonths(NumberOfMonths);
    }

    // Give startdate, create function that return the month of the starting month
    public int GetStartMonth()
    {
        return StartDate.Month;
    }
}