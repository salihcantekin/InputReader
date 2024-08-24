using FluentAssertions;
using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using Moq;

namespace InputReaderUnitTest.InputReaders;

[TestFixture]
internal class CharInputReaderTests
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
        var readLine = "a";
        ConfigureMockReader(readLine);

        // Act
        var charValue = BuildCharReader().Read();

        // Assert
        mockReader.Verify(i => i.Read(), Times.Once);
    }

    [Test]
    public void Read_WithValidValue_ShouldNotReturnNull()
    {
        // Arrange
        var readLine = "a";
        ConfigureMockReader(readLine);

        // Act
        var charValue = BuildCharReader().Read();

        // Assert
        charValue.Should().NotBeNull();
    }

    [Test]
    public void Read_WithValidValue_ShouldReturnExpectedValue()
    {
        // Arrange
        var readLine = "a";
        var expected = 'a';
        ConfigureMockReader(readLine);

        // Act
        var charValue = BuildCharReader().Read();

        // Assert
        charValue.Value.Should().Be(expected);
    }

    [Test]
    public void Read_WithValidValue_ShouldReturnIsValidTrue()
    {
        // Arrange
        var readLine = "a";
        ConfigureMockReader(readLine);

        // Act
        var charValue = BuildCharReader().Read();

        // Assert
        charValue.IsValid.Should().BeTrue();
    }

    [Test]
    public void Read_WithEmptyValue_ShouldReturnIsValidFalse()
    {
        // Arrange
        var readLine = "";
        ConfigureMockReader(readLine);

        // Act
        var charValue = BuildCharReader().Read();

        // Assert
        charValue.IsValid.Should().BeFalse();
    }

    [Test]
    public void Read_WithValidValue_ShouldReturnNullableChar()
    {
        // Arrange
        var readLine = "a";
        var expected = 'a';
        ConfigureMockReader(readLine);

        // Act
        var charValue = BuildCharReader().Read();

        // Assert
        char? value = charValue;
        value.Should().Be(expected);
    }

    [Test]
    public void Read_WithEmptyValue_ShouldReturnNullChar()
    {
        // Arrange
        var readLine = "";
        var expected = (char?)null;
        ConfigureMockReader(readLine);

        // Act
        var charValue = BuildCharReader().Read();

        // Assert
        char? value = charValue;
        value.Should().Be(expected);
    }

    #region Private Methods

    private IInputReader<char?, CharInputValue> BuildCharReader()
    {
        return new CharInputReader().With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(string readLine)
    {
        mockReader.Setup(i => i.Read()).Returns(readLine);
    }

    #endregion
}

