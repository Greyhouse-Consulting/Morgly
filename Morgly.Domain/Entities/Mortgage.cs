using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using Morgly.Domain.Events;

namespace Morgly.Domain.Entities
{

    // Create baseclass for domain entities that stores domain events and has a method for adding domain events
    public abstract class Entity
    {
        [NotMapped]
        private readonly List<DomainEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }

    public class DomainEvent : INotification
    {
    }


    /// <summary>
    /// Represents a mortgage loan.
    /// </summary>
    /// <remarks>
    /// This class contains properties and methods for managing a mortgage loan, including calculating payments and adding terms.
    /// </remarks>
    public class Mortgage(Guid id, decimal amount, Term initialTerm) : Entity
    {
        private readonly IList<Term> _terms = new List<Term> { initialTerm };
        public Guid Id { get; } = id;

        private decimal _amount = amount;
        public decimal Amount => _amount;
        
        private decimal _originalAmount = amount;
        public decimal OriginalAmount => _originalAmount;

        public List<Payment> Payments { get; } = new();

        // Create method that adds a term to mortgage. It is not allowed to add a new term if the last term intersects with the new term.

        
 

        //// Create method that adds a term to mortgage. It is not allowed to add a new term if the last term intersects with the new term.
        //public void AddTerm(Term term)
        //{
        //    var lastTerm = _terms[^1];  

        //    if (lastTerm.GetEndDate() > term.StartDate)
        //    {
        //        throw new Exception("New term cannot start before the end of the last term");
        //    }

        //    _terms.Add(term);

        //    AddDomainEvent(new MortgageTermAddedEvent(Id, term.StartDate, term.NumberOfMonths));
        //}


        // Create method that adds a term to mortgage. It is not allowed to add a new term if it not is directly after the last term in on a monthly basis. Day should not be part of the comparison.
        //public void AddTerm(Term term)
        //{
        //    var lastTerm = _terms[^1];
        //    var expectedStartDate = lastTerm.GetEndDate().AddMonths(1);


        //    if (!AreDatesEqualIgnoringDay(expectedStartDate, term.StartDate))
        //    {
        //        throw new Exception("New term must start directly after the end of the last term");
        //    }

        //    _terms.Add(term);

        //    AddDomainEvent(new MortgageTermAddedEvent(Id, term.StartDate, term.NumberOfMonths));
        //}

        public void AddSequentialTerm(Term term)
        {
            var lastTerm = _terms[^1];
            var lastTermEndDate = new DateTime(lastTerm.GetEndDate().Year, lastTerm.GetEndDate().Month, 1);
            var newTermStartDate = new DateTime(term.StartDate.Year, term.StartDate.Month, 1);
            if (lastTermEndDate.AddMonths(1) != newTermStartDate)
            {
                throw new Exception("New term must start directly after the last term on a monthly basis");
            }
            _terms.Add(term);
            AddDomainEvent(new MortgageTermAddedEvent(Id, term.StartDate, term.NumberOfMonths));
        }


        // Compare two dates and return true if they are equal ignoring day of month
        private bool AreDatesEqualIgnoringDay(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month;
        }
        
        // Create a function that calculates the upcoming months payment including interest and amortization for a given period
        public Payment CalculatePayment(DateTime date, decimal monthlyPrincipal)
        {
            var monthlyInterest = Amount * (_terms[0].InterestRate / 100) / 12;
            var monthlyPayment = monthlyInterest + monthlyPrincipal;

            return new Payment(monthlyPayment, date, _terms[0].GetEndDate());
        }


        public List<MonthlyPayment> CalculateMonthlyPayments(decimal monthlyPrincipal, int numberOfMonths)
        {
            var monthlyPayments = new List<MonthlyPayment>();
            var remainingAmount = Amount;
            for (var i = 0; i < numberOfMonths; i++)
            {
                var monthlyInterest = remainingAmount * (_terms[^1].InterestRate / 100) / 12;
                var monthlyPayment = monthlyInterest + monthlyPrincipal;
                remainingAmount -= monthlyPrincipal;
                monthlyPayments.Add(new MonthlyPayment(monthlyPayment, monthlyInterest, monthlyPrincipal, remainingAmount));
            }
            return monthlyPayments;
        }




        // Function that calculates the comming 12 monthly payments for a mortgage using interest rate and remainingAmount from this class



        // Create a function that calculates the number of periods (months) until mortgage is paid off taking monthly payment as input

        public int CalculateNumberOfPeriods(decimal monthlyPayment)
        {
            var remainingAmount = Amount;
            var numberOfPeriods = 0;

            while (remainingAmount > 0)
            {
                var monthlyInterest = remainingAmount * (_terms[0].InterestRate / 100) / 12;
                var monthlyPrincipal = monthlyPayment - monthlyInterest;
                remainingAmount -= monthlyPrincipal;
                numberOfPeriods++;
            }

            return numberOfPeriods;
        }

        public Term GetLastTerm() => _terms[^1];

        public decimal GetInterestRate()
        {
            return GetLastTerm().InterestRate;
        }

        public void AddTerm(DateTime startDate, int lengthInMonths, decimal interestRate) =>
            AddSequentialTerm(new Term(startDate, lengthInMonths, interestRate));
        }

    public class MonthlyPayment(
        decimal amount,
        decimal monthlyInterest,
        decimal monthlyPrincipal,
        decimal remainingAmount)
    {
        public decimal MonthlyInterest { get; } = monthlyInterest;
        public decimal MonthlyPrincipal { get; } = monthlyPrincipal;
        public decimal RemainingAmount { get; } = remainingAmount;
        public decimal Amount { get; } = amount;
    }
}
