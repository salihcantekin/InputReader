using InputReader.InputReaders;
using InputReader;
using InputReader.InputReaders.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InputReader.Converters.CustomConverters;
using FluentAssertions;
using InputReader.Converters;

namespace InputReaderUnitTest.InputReaders;
internal class DateOnlyReaderTests
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
        var readLine = "2022-01-01";
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader().Read();

        // Assert
        mockReader.Verify(i => i.ReadLine(), Times.Once);
    }

    [Test]
    public void Read_WithValidValue_ShouldNotReturnNull()
    {
        // Arrange
        var readLine = "2022-01-01";
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader().Read();

        // Assert
        dateValue.Should().NotBeNull();
    }


    [Test]
    public void Read_WithValidValue_ShouldReturnExpectedValue()
    {
        // Arrange
        var readLine = "2022-01-01";
        var expected = new CustomDateOnly(2022, 1, 1);
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader().Read();

        // Assert
        dateValue.Value.Should().Be(expected);
    }

    [Test]
    public void Read_WithInvalidValue_ShouldReturnNull()
    {
        // Arrange
        var readLine = "Invalid";
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader().Read();

        // Assert
        dateValue.Value.Should().BeNull();
    }

    [Test]
    public void Read_WithInvalidValue_ShouldReturnIsValidFalse()
    {
        // Arrange
        var readLine = "Invalid";
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader().Read();

        // Assert
        dateValue.IsValid.Should().BeFalse();
    }

    [Test]
    public void Read_WithEmptyValue_ShouldReturnIsValidFalse()
    {
        // Arrange
        var readLine = "";
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader().Read();

        // Assert
        dateValue.IsValid.Should().BeFalse();
    }

    [Test]
    public void Read_WithEmptyValue_ShouldReturnNull()
    {
        // Arrange
        var readLine = "";
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader().Read();

        // Assert
        dateValue.Value.Should().BeNull();
    }

    [Test]
    public void Read_WithInvalidValue_ShouldReturnNullValue()
    {
        // Arrange
        var readLine = "Invalid";
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader().Read();

        // Assert
        dateValue.Value.Should().BeNull();
    }

    [Test]
    public void Read_WithValidFormatButInvalidDate_ShouldReturnNull()
    {
        // Arrange
        var readLine = "2022-01-32";
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader().Read();

        // Assert
        dateValue.Value.Should().BeNull();
    }

    [Test]
    public void Read_WithValidFormatButInvalidDate_ShouldReturnIsValidFalse()
    {
        // Arrange
        var readLine = "2022-01-32";
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader().Read();

        // Assert
        dateValue.IsValid.Should().BeFalse();
    }

    [Test]
    public void Read_WithValidFormat_ShouldReturnExpectedFormatValue()
    {
        // Arrange
        var format = "dd.MM.yyyy";
        var readLine = "20.05.1990";
        var expected = new CustomDateOnly(1990, 5, 20);
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader(format).Read();

        // Assert
        dateValue.Value.Should().Be(expected);
    }

    [Test]
    public void Read_WithValidFormat_ShouldReturnIsValidTrue()
    {
        // Arrange
        var format = "dd.MM.yyyy";
        var readLine = "20.05.1990";
        ConfigureMockReader(readLine);

        // Act
        var dateValue = BuildDateOnlyReader(format).Read();

        // Assert
        dateValue.IsValid.Should().BeTrue();
    }

    [Test]
    public void Read_WithValidFormat_ShouldReturnExpectedFormatValue2()
    {
        // Arrange
        var format = "yyyy-MM-dd";
        var readLine = "1990-05-20";
        var expected = new CustomDateOnly(1990, 5, 20);
        ConfigureMockReader(readLine);
        
        // Act
        var dateValue = BuildDateOnlyReader(format).Read();

        // Assert
        dateValue.Value.Should().Be(expected);
    }




    #region Private Methods

    private IInputReader<CustomDateOnly?, DateOnlyInputValue> BuildDateOnlyReader(string format = Constants.Format.DateFormat)
    {
        return new DateOnlyInputReader("", format: format).With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(string readLine)
    {
        mockReader.Setup(i => i.ReadLine()).Returns(readLine);
    }

    #endregion
}
