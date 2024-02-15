namespace Morgly.Domain.Entities;

// Create extension class TermDate and create method that converts a datetime to a termdate
public static class TermDateExtensions
{
    public static TermDate ToTermDate(this DateTime dateTime)
    {
        return new TermDate(dateTime.Year, dateTime.Month);
    }
}


// Create public class TermDate that represent the month and year of a term
public class TermDate(int year, int month) : IEquatable<TermDate>
{
    private readonly DateOnly _date = new(year, month, 1);

    public int Month => _date.Month;
    public int Year => _date.Year;

    public bool Equals(TermDate? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _date.Equals(other._date);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TermDate)obj);
    }

    public override int GetHashCode()
    {
        return _date.GetHashCode();
    }

    public TermDate AddMonths(int numberOfMonths)
    {
        return new TermDate(_date.AddMonths(numberOfMonths).Year, _date.AddMonths(numberOfMonths).Month);
    }

    public DateOnly ToDateOnly()
    {
        return _date;
    }
}

// create public class Term with startdate and number of months as properties   
public class Term(TermDate startDate, int numberOfMonths, decimal interestRate)
{
    public TermDate StartDate { get; } = startDate;
    public int NumberOfMonths { get; } = numberOfMonths;
    public decimal InterestRate { get; } = interestRate;

    public Term(DateOnly startDate, int numberOfMonths, decimal interestRate)
        : this(new TermDate(startDate.Year, startDate.Month), numberOfMonths, interestRate)
    {
    }

    // Given start date create function that return the end date of the term using number of months 
    public TermDate GetEndDate()
    {
        return StartDate.AddMonths(NumberOfMonths);
    }

    // Give startdate, create function that return the month of the starting month
    public int GetStartMonth()
    {
        return StartDate.Month;
    }
}