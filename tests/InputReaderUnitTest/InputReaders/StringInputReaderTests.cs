using FluentAssertions;
using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using Moq;

namespace InputReaderUnitTest.InputReaders;

[TestFixture]
internal class StringInputReaderTests
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
        var readLine = "test";
        ConfigureMockReader(readLine);

        // Act
        var stringValue = BuildStringReader().Read();

        // Assert
        mockReader.Verify(i => i.ReadLine(), Times.Once);
    }

    [Test]
    public void Read_WithValidValue_ShouldNotReturnNull()
    {
        // Arrange
        var readLine = "test";
        ConfigureMockReader(readLine);

        // Act
        var stringValue = BuildStringReader().Read();

        // Assert
        stringValue.Should().NotBeNull();
    }

    [Test]
    public void Read_WithValidValue_ShouldReturnExpectedValue()
    {
        // Arrange
        var readLine = "test";
        var expected = "test";
        ConfigureMockReader(readLine);

        // Act
        var stringValue = BuildStringReader().Read();

        // Assert
        stringValue.Value.Should().Be(expected);
    }

    [Test]
    public void Read_WithValidValue_ShouldReturnIsValidTrue()
    {
        // Arrange
        var readLine = "test";
        ConfigureMockReader(readLine);

        // Act
        var stringValue = BuildStringReader().Read();

        // Assert
        stringValue.IsValid.Should().BeTrue();
    }

    [Test]
    public void Read_WithEmptyValue_ShouldReturnIsValidFalse()
    {
        // Arrange
        var readLine = "";
        ConfigureMockReader(readLine);

        // Act
        var stringValue = BuildStringReader().Read();

        // Assert
        stringValue.IsValid.Should().BeFalse();
    }

    [Test]
    public void Read_WithValidValue_ShouldReturnNullableString()
    {
        // Arrange
        var readLine = "test";
        var expected = "test";
        ConfigureMockReader(readLine);

        // Act
        var stringValue = BuildStringReader().Read();

        // Assert
        string? value = stringValue;
        value.Should().Be(expected);
    }

    [Test]
    public void Read_WithEmptyValue_ShouldReturnNullString()
    {
        // Arrange
        var readLine = "";
        var expected = (string?)null;
        ConfigureMockReader(readLine);

        // Act
        var stringValue = BuildStringReader().Read();

        // Assert
        string? value = stringValue;
        value.Should().Be(expected);
    }

    #region Private Methods

    private IInputReader<string?, StringInputValue> BuildStringReader()
    {
        return new StringInputReader().With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(string readLine)
    {
        mockReader.Setup(i => i.ReadLine()).Returns(readLine);
    }

    #endregion
}
