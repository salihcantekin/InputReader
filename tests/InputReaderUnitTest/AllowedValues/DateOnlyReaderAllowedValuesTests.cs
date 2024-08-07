using FluentAssertions;
using InputReader;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using Moq;

namespace InputReaderUnitTest.AllowedValues;
internal class DateOnlyReaderAllowedValuesTests
{
    private readonly Mock<IInputReaderBase> mockReader;

    public DateOnlyReaderAllowedValuesTests()
    {
        mockReader = new Mock<IInputReaderBase>();
    }

    [Test]
    [TestCase("2023-10-01", ExpectedResult = true)]
    [TestCase("2023-10-02", ExpectedResult = false)]
    public bool Read_WithAllowedValue_ShouldReturnExpectedIsValid(string input)
    {
        // Arrange
        ConfigureMockReader(input);

        var reader = BuildDateOnlyReader().WithAllowedValues(new CustomDateOnly(2023, 10, 01));

        // Action
        var value = reader.Read();

        // return
        return value.IsValid;
    }

    [Test]
    [TestCase("2023-10-32", ExpectedResult = null)]
    [TestCase("2023-09-01", ExpectedResult = null)]
    [TestCase("1.1.1990", ExpectedResult = null)]
    [TestCase("", ExpectedResult = null)]
    public CustomDateOnly? Read_WithParamsNotAllowedValue_ShouldReturnNull(string input)
    {
        // Arrange
        ConfigureMockReader(input);
        var reader = BuildDateOnlyReader().WithAllowedValues(allowedValues: new CustomDateOnly(2023, 10, 01));

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }



    [Test]
    public void Read_WithParamsAllowedValue_ShouldReturnExpectedValue()
    {
        // Arrange
        string input = "2023-10-01";
        var expected = CustomDateOnly.From(2023, 10, 1);
        ConfigureMockReader(input);
        var reader = BuildDateOnlyReader().WithAllowedValues(new CustomDateOnly(2023, 10, 01));

        // Action
        var value = reader.Read();

        // Assert
        value.Value.Should().Be(expected);
    }




    [Test]
    [TestCase("2023-10-32", ExpectedResult = null)]
    [TestCase("2023-09-01", ExpectedResult = null)]
    [TestCase("1.1.1990", ExpectedResult = null)]
    [TestCase("", ExpectedResult = null)]
    public CustomDateOnly? Read_WithDateOnlyIEnumerableNotAllowedValue_ShouldReturnNull(string input)
    {
        // Arrange
        ConfigureMockReader(input);
        IEnumerable<CustomDateOnly?> enumerableValues = [new CustomDateOnly(2023, 10, 01)];
        var reader = BuildDateOnlyReader().WithAllowedValues(allowedValues: enumerableValues);

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }

    [Test]
    public void Read_WithDateOnlyEnumerableAllowedValue_ShouldReturnExpectedValue()
    {
        // Arrange
        var input = "2023-10-01";
        var expected = CustomDateOnly.From(2023, 10, 1);
        ConfigureMockReader(input);
        IEnumerable<CustomDateOnly?> enumerableValues = [new CustomDateOnly(2023, 10, 01)];
        var reader = BuildDateOnlyReader().WithAllowedValues(enumerableValues);

        // Action
        var value = reader.Read();

        // Assert
        value.Value.Should().Be(expected);
    }


    #region Private Methods

    private DateOnlyInputReader BuildDateOnlyReader()
    {
        return (DateOnlyInputReader)new DateOnlyInputReader()
                                    .ClearAllowedValues()
                                    .With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(string readLine)
    {
        mockReader.Setup(i => i.Read()).Returns(readLine);
    }

    #endregion
}
