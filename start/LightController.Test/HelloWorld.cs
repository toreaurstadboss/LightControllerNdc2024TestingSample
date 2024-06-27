using FluentAssertions;

namespace LightController.Test;

public class HelloWorld
{
    [Fact]
    public void HelloTest()
    {
        //Arrange
        int expected = 7;

        //Act
        int actual = 7;

        actual.Should().Be(expected);

    }
}