using FluentAssertions;
using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using Moq;

namespace InputReaderUnitTest.InputReaders;

[TestFixture]
internal class IntInputReaderWithConsoleReaderTests
{
    private Mock<IInputReaderBase> mockReader;

    [SetUp]
    public void Setup()
    {
        mockReader = new Mock<IInputReaderBase>();
    }

    [Test]
    public void Read_WhenCalled_ShouldCallReadLine()
    {
        // Arrange
        var readLine = "10";
        ConfigureMockReader(readLine);

        // Act
        var intValue = BuildIntReader().Read();

        // Assert
        mockReader.Verify(i => i.ReadLine(), Times.Once);
    }

    [Test]
    public void Read_WithValidValue_ShouldNotReturnNull()
    {
        // Arrange
        var readLine = "10";
        ConfigureMockReader(readLine);

        // Act
        var intValue = BuildIntReader().Read();

        // Assert
        intValue.Should().NotBeNull();
    }

    [Test]
    public void Read_WithValidValue_ShouldReturnExpectedValue()
    {
        // Arrange
        var readLine = "10";
        var expected = 10;
        ConfigureMockReader(readLine);

        // Act
        var intValue = BuildIntReader().Read();

        // Assert
        intValue.Value.Should().Be(expected);
    }

    [Test]
    public void Read_WithValidValue_ShouldReturnIsValidTrue()
    {
        // Arrange
        var readLine = "10";
        ConfigureMockReader(readLine);

        // Act
        var intValue = BuildIntReader().Read();

        // Assert
        intValue.IsValid.Should().BeTrue();
    }

    [Test]
    public void Read_WithInvalidValue_ShouldReturnIsValidFalse()
    {
        // Arrange
        var readLine = "abc";
        ConfigureMockReader(readLine);

        // Act
        var intValue = BuildIntReader().Read();

        // Assert
        intValue.IsValid.Should().BeFalse();
    }

    [Test]
    public void Read_WithValidValue_ShouldReturnNullableInt()
    {
        // Arrange
        var readLine = "10";
        var expected = 10;
        ConfigureMockReader(readLine);

        // Act
        var intValue = BuildIntReader().Read();

        // Assert
        int? value = intValue;
        value.Should().Be(expected);
    }

    [Test]
    public void Read_WithInvalidValue_ShouldReturnNullInt()
    {
        // Arrange
        var readLine = "abc";
        var expected = (int?)null;
        ConfigureMockReader(readLine);

        // Act
        var intValue = BuildIntReader().Read();

        // Assert
        int? value = intValue;
        value.Should().Be(expected);
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