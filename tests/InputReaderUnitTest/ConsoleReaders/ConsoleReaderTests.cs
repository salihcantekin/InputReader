using FluentAssertions;
using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using Moq;

namespace InputReaderUnitTest.ConsoleReaders;

internal class ConsoleReaderTests
{
    private readonly Mock<IInputReaderBase> mockReader;

    public ConsoleReaderTests()
    {
        mockReader = new Mock<IInputReaderBase>();
    }

    [Test]
    public void Read_WhenCalled_ShouldCallRead()
    {
        // Arrange
        var readLine = "10";
        ConfigureMockReader(readLine);

        // Action
        var intValue = BuildIntReader().Read();

        // Assert
        mockReader.Verify(i => i.ReadLine(), Times.Once);
    }

    [Test]
    public void Read_WhenCalled_ShouldReturnExpected()
    {
        // Arrange
        var readLine = "10";
        var expected = 10;
        ConfigureMockReader(readLine);

        // Action
        var intValue = BuildIntReader().Read();

        // Assert
        intValue.Value.Should().Be(expected);
    }

    [Test]
    public void Read_WhenCalled_ShouldReturnExpectedValue()
    {
        // Arrange
        var readLine = "10";
        var expected = 10;
        ConfigureMockReader(readLine);

        // Action
        var intValue = BuildIntReader().Read();

        // Assert
        intValue.Value.Should().Be(expected);
    }

    [Test]
    public void Read_WhenCalled_ShouldReturnExpectedIsValid()
    {
        // Arrange
        var readLine = "10";
        ConfigureMockReader(readLine);

        // Action
        var intValue = BuildIntReader().Read();

        // Assert
        intValue.IsValid.Should().BeTrue();
    }

    [Test]
    public void Read_WhenCalledWithInvalidInt_ShouldReturnExpectedIsValid()
    {
        // Arrange
        var readLine = "Invalid";
        ConfigureMockReader(readLine);

        // Action
        var intValue = BuildIntReader().Read();

        // Assert
        intValue.IsValid.Should().BeFalse();
    }

    [Test]
    public void Read_WhenCalledWithInvalidInt_ShouldReturnExpectedValue()
    {
        // Arrange
        var readLine = "Invalid";
        var expected = (int?)null;
        ConfigureMockReader(readLine);

        // Action
        var intValue = BuildIntReader().Read();

        // Assert
        intValue.Value.Should().Be(expected);
    }

    #region Private Methods

    private IInputReader<int?, IntInputValue> BuildIntReader()
    {
        return new IntInputReader().With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(string readLine)
    {
        mockReader.Setup(i => i.ReadLine()).Returns(readLine);
    }

    #endregion
}
