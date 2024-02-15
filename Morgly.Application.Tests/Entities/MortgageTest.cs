using AutoFixture;
using Morgly.Domain.Entities;
using Shouldly;

namespace Morgly.Application.Tests.Entities
{
    public class MortgageTest
    {
        private readonly Fixture _fixture = new();

        [Fact]
        public void AddTerm_ShouldAddTerm_WhenTermStartsDirectlyAfterLastTerm()
        {
            // Arrange
            var sut = new Mortgage(Guid.NewGuid(), 100000, new Term(DateTime.Now.ToTermDate(), 12, 3.5m));
            var lastTerm = sut.GetLastTerm();
            var newTerm = new Term(lastTerm.GetEndDate().AddMonths(1), 12, 5);

            // Act
            sut.AddTerm(lastTerm.GetEndDate().AddMonths(1).ToDateOnly(), 12, 5);

            // Assert
            Term term = sut.GetLastTerm();
            term.ShouldBe(newTerm);
        }

        [Fact]
        public void AddTerm_ShouldThrowException_WhenTermDoesNotStartDirectlyAfterLastTerm()
        {
            // Arrange
            var sut = _fixture.Create<Mortgage>();
            var lastTerm = sut.GetLastTerm();

            // Act & Assert
            Assert.Throws<Exception>(() => sut.AddTerm(lastTerm.GetEndDate().AddMonths(2).ToDateOnly(), 12, 5));
        }

        [Fact]
        public void CalculatePayment_ShouldReturnCorrectPayment()
        {
            // Arrange
            var sut = _fixture.Create<Mortgage>();
            var date = DateTime.Now;
            var monthlyPrincipal = 1000m;

            // Act
            var payment = sut.CalculatePayment(date, monthlyPrincipal);

            // Assert
            payment.ShouldNotBeNull();
            payment.Amount.ShouldBe(monthlyPrincipal + sut.Amount * (sut.GetInterestRate() / 100) / 12);
            payment.Date.ShouldBe(date);
        }

        [Fact]
        public void CalculateMonthlyPayments_ShouldReturnCorrectNumberOfPayments()
        {
            // Arrange
            var sut = _fixture.Create<Mortgage>();
            var monthlyPrincipal = 1000m;
            var numberOfMonths = 12;

            // Act
            var payments = sut.CalculateMonthlyPayments(monthlyPrincipal, numberOfMonths);

            // Assert
            payments.Count.ShouldBe(numberOfMonths);
        }

        [Fact]
        public void CalculateNumberOfPeriods_ShouldReturnCorrectNumberOfPeriods()
        {
            // Arrange
            var sut = _fixture.Create<Mortgage>();
            var monthlyPayment = 1000m;

            // Act
            var numberOfPeriods = sut.CalculateNumberOfPeriods(monthlyPayment);

            // Assert
            numberOfPeriods.ShouldBeGreaterThan(0);
        }
    }
}