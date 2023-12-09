using AutoFixture;
using MediatR;
using Moq;
using Morgly.Application.Features.Mortgage.Command;
using Morgly.Application.Interfaces;
using Shouldly;

namespace Morgly.Application.Tests;




public class Should_create_mortgage
{
    // Create unittet for CreatemortgageCommandhandler
    //[Fact]
    //public async Task Given_CreateMortgageCommand_When_handling_correct_command_Then_Create_mortgage()
    //{
    //    // Arrange
    //    var fixture = new Fixture();
    //    var uowMock = new Mock<IUnitOfWork>();
    //    var mediatorMock = new Mock<IMediator>();
    //    var sut = new CreateMortgageCommandHandler(uowMock.Object, mediatorMock.Object);

    //    var command = fixture.Build<CreateMortgageCommand>().FromFactory<string,decimal,int>((name, interestRate, termInMonths) 
    //        => new CreateMortgageCommand(name, interestRate, 1000000, termInMonths))
    //        .Create();

    //    // Act
    //    var result = await sut.Handle(command, CancellationToken.None);

    //    // Assert
    //    uowMock.Verify(x => x.Add(It.IsAny<Domain.Entities.Mortgage>()), Times.Once);
    //    uowMock.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Once);
    //    mediatorMock.Verify(x => x.Publish(It.IsAny<IMortgageCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    //    result.ShouldNotBe(Guid.Empty);

    //    // Update test
    //    result.ShouldBeOfType<Guid>();
    //}




    // Test that CreateMortgageCommandHandler throws exception when amount is to large
    //[Fact]
    //public async Task
    //    Given_CreateMortgageCommand_When_handling_command_with_amount_greater_than_1000000_Then_throw_exception()
    //{
    //    // Arrange
    //    var fixture = new Fixture();
    //    var uowMock = new Mock<IUnitOfWork>();
    //    var mediatorMock = new Mock<IMediator>();
    //    var sut = new CreateMortgageCommandHandler(uowMock.Object, mediatorMock.Object);

    //    var command = fixture.Build<CreateMortgageCommand>().FromFactory<string, decimal, int>((name, interestRate, termInMonths)
    //                   => new CreateMortgageCommand(name, interestRate, 1000001, termInMonths))
    //        .Create();

    //    // Act
    //    var result = await Should.ThrowAsync<Exception>(async () => await sut.Handle(command, CancellationToken.None));

    //    // Assert
    //    result.Message.ShouldBe("monthlyPayment cannot be greater than 1000000");
    //}


    // Test that CreateMortgageCommandHandler publish event when mortgage is created
    //[Fact]
    //public async Task
    //    Given_CreateMortgageCommand_When_handling_correct_command_Then_publish_event()
    //{
    //    // Arrange
    //    var fixture = new Fixture();
    //    var uowMock = new Mock<IUnitOfWork>();
    //    var mediatorMock = new Mock<IMediator>();
    //    var sut = new CreateMortgageCommandHandler(uowMock.Object, mediatorMock.Object);

    //    var command = fixture.Build<CreateMortgageCommand>().FromFactory<string, decimal, int>((name, interestRate, termInMonths)
    //                              => new CreateMortgageCommand(name, interestRate, 1000000, termInMonths))
    //        .Create();

    //    // Act
    //    var result = await sut.Handle(command, CancellationToken.None);

    //    // Assert
    //    mediatorMock.Verify(x => x.Publish(It.IsAny<IMortgageCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    //}
}
