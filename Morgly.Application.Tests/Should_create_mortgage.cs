using AutoFixture;
using MediatR;
using Moq;
using Morgly.Application.Features.Mortgage.Command;
using Morgly.Application.Interfaces;
using Morgly.Domain.Repositories;
using Shouldly;

namespace Morgly.Application.Tests;


// Generate tests  for CreateMortgageCommandHandler
public class Should_create_mortgage
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IUnitOfWork> _uowMock = new();
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly Mock<IMortgageRepository> _mortgageRepositoryMock = new();
    private readonly Mock<IEventRepository> _eventRepositoryMock = new();
    private readonly Mock<TransactionIdHolder> _transactionIdHolderMock = new();

    //[Fact]
    //public async Task When_amount_is_less_than_1000000()
    //{
    //    // Arrange
    //    var command = _fixture.Create<CreateMortgageCommand>();
    //    var sut = new CreateMortgageCommandHandler(
    //                   _uowMock.Object,
    //                              _mediatorMock.Object,
    //                              _mortgageRepositoryMock.Object,
    //                              _eventRepositoryMock.Object,
    //                              _transactionIdHolderMock.Object);

    //    // Act
    //    await sut.Handle(command, CancellationToken.None);

    //    // Assert
    //    _mortgageRepositoryMock.Verify(x => x.Add(It.IsAny<Domain.Entities.Mortgage>()), Times.Once);
    //    _uowMock.Verify(x => x.SaveChanges(CancellationToken.None), Times.Once);
    //    _mediatorMock.Verify(x => x.Publish(It.IsAny<MortgageCreatedEvent>(), CancellationToken.None), Times.Once);
    //}

    //[Fact]
    //public async Task When_amount_is_greater_than_1000000()
    //{
    //    // Arrange
    //    var command = new CreateMortgageCommand ("asd", 2m, 1000001, 12);
    //    var sut = new CreateMortgageCommandHandler(
    //                   _uowMock.Object,
    //                              _mediatorMock.Object,
    //                              _mortgageRepositoryMock.Object,
    //                              _eventRepositoryMock.Object,
    //                              _transactionIdHolderMock.Object);

    //    // Act
    //    var exception = await Should.ThrowAsync<Exception>(async () => await sut.Handle(command, CancellationToken.None));

    //    // Assert
    //    exception.Message.ShouldBe("monthlyPayment cannot be greater than 1000000");
    //    _mortgageRepositoryMock.Verify(x => x.Add(It.IsAny<Domain.Entities.Mortgage>()), Times.Never);
    //    _uowMock.Verify(x => x.SaveChanges(CancellationToken.None), Times.Never);
    //    _mediatorMock.Verify(x => x.Publish(It.IsAny<MortgageCreatedEvent>(), CancellationToken.None), Times.Never);
    //}
}
