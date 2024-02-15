using Xunit;
using Morgly.Application;

public class DogTests
{
    [Fact]
    public void GetSound_ReturnsWoof()
    {
        // Arrange
        var dog = new Dog();

        // Act
        var result = dog.GetSound();

        // Assert
        Assert.Equal("Woof", result);
    }
}
