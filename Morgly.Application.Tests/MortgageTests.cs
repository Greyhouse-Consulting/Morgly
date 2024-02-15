using Xunit;
using Morgly.Domain.Entities;
using System;

public class MortgageTests
{
    [Fact]
    public void AddSequentialTerm_ThrowsException_WhenNewTermDoesNotStartAfterLastTerm()
    {
        // Arrange
        var mortgage = new Mortgage(Guid.NewGuid(), 100000, new Term(DateTime.Now.ToTermDate(), 12, 3.5m));
        var newTerm = new Term(DateTime.Now.AddMonths(14).ToTermDate(), 12, 3.5m);

        // Act & Assert
        Assert.Throws<Exception>(() => mortgage.AddSequentialTerm(newTerm));
    }

    [Fact]
    public void CalculatePayment_ReturnsCorrectPayment_WhenGivenValidInputs()
    {
        // Arrange
        var mortgage = new Mortgage(Guid.NewGuid(), 100000, new Term(DateTime.Now.ToTermDate(), 12, 3.5m));
        var date = DateTime.Now.AddMonths(1);
        var monthlyPrincipal = 1000;

        // Act
        var payment = mortgage.CalculatePayment(date, monthlyPrincipal);

        // Assert
        Assert.Equal(monthlyPrincipal + (mortgage.Amount * (mortgage.GetInterestRate() / 100) / 12), payment.Amount);
    }

    [Fact]
    public void CalculateNumberOfPeriods_ReturnsCorrectNumberOfPeriods_WhenGivenValidInputs()
    {
        // Arrange
        var mortgage = new Mortgage(Guid.NewGuid(), 100000, new Term(DateTime.Now.ToTermDate(), 12, 3.5m));
        var monthlyPayment = 1000;

        // Act
        var numberOfPeriods = mortgage.CalculateNumberOfPeriods(monthlyPayment);

        // Assert
        Assert.True(numberOfPeriods > 0);
    }
}
