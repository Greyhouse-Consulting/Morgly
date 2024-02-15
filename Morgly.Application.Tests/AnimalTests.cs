using Xunit;
using Morgly.Application;

public class AnimalTests
{
    [Fact]
    public void AnimalConstructor_SetsNumberOfLegsAndSound()
    {
        // Arrange
        var numberOfLegs = 4;
        var sound = "Woof";

        // Act
        var animal = new Dog();

        // Assert
        Assert.Equal(numberOfLegs, animal.GetNumberOfLegs());
        Assert.Equal(sound, animal.GetSound());
    }
}
