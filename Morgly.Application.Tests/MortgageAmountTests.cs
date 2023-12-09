using AutoFixture;
using Morgly.Domain.Entities;
using Shouldly;

namespace Morgly.Application.Tests;

public class MortgageAmountTests
{
    // Create test to verify Mortgage calculates 12 monthly payments and that they are correct
    public class MortgageTests
    {
    }

    [Fact]
    public void Given_Mortgage_When_calculating_monthly_payments_Then_return_12_monthly_payments()
    {
        // Arrange
        var fixture = new Fixture();
        var sut = fixture.Build<Mortgage>().FromFactory<decimal>((amount) => new Domain.Entities.Mortgage(Guid.NewGuid(), amount, new Term(DateTime.Now, 12, 5)))
            .Create();

        // Act
        var result = sut.CalculateMonthlyPayments(1000, 12);

        var c = sut.CalculateNumberOfPeriods(10);
        // Assert
        result.Count.ShouldBe(12);
    }

    [Fact]
    public void Given_Mortgage_When_calculating_monthly_payments_Then_return_correct_monthly_payments()
    {
        // Arrange
        var fixture = new Fixture();
        var sut = fixture.Create<Mortgage>();

        // Act
        var result = sut.CalculateMonthlyPayments(1000, 12);

        // Assert
        result[0].Amount.ShouldBe(84386.95m);
        result[0].MonthlyInterest.ShouldBe(4166.67m);
        result[0].MonthlyPrincipal.ShouldBe(80220.28m);
        result[0].RemainingAmount.ShouldBe(919779.72m);

        result[1].Amount.ShouldBe(84386.95m);
        result[1].MonthlyInterest.ShouldBe(3832.41m);
        result[1].MonthlyPrincipal.ShouldBe(80554.54m);
        result[1].RemainingAmount.ShouldBe(839225.18m);

        result[2].Amount.ShouldBe(84386.95m);
        result[2].MonthlyInterest.ShouldBe(3497.69m);
        result[2].MonthlyPrincipal.ShouldBe(80989.26m);
        result[2].RemainingAmount.ShouldBe(758235.92m);

        result[3].Amount.ShouldBe(84386.95m);
        result[3].MonthlyInterest.ShouldBe(3162.41m);
        result[3].MonthlyPrincipal.ShouldBe(81524.54m);
        result[3].RemainingAmount.ShouldBe(676711.38m);

        result[4].Amount.ShouldBe(84386.95m);
        result[4].MonthlyInterest.ShouldBe(2826.56m);
        result[4].MonthlyPrincipal.ShouldBe(82160.39m);
        result[4].RemainingAmount.ShouldBe(594550.99m);

        result[5].Amount.ShouldBe(84386.95m);
        result[5].MonthlyInterest.ShouldBe(2490.13m);
        result[5].MonthlyPrincipal.ShouldBe(82796.82m);
        result[5].RemainingAmount.ShouldBe(511754.17m);

        result[6].Amount.ShouldBe(84386.95m);
        result[6].MonthlyInterest.ShouldBe(2153.12m);
        result[6].MonthlyPrincipal.ShouldBe(83433.83m);
        result[6].RemainingAmount.ShouldBe(428320.34m);

        result[7].Amount.ShouldBe(84386.95m);

    }

}
