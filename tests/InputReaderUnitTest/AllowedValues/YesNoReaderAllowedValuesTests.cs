using InputReader;
using InputReader.InputReaders.Interfaces;
using Moq;

namespace InputReaderUnitTest.AllowedValues;
internal class YesNoReaderAllowedValuesTests
{
    private readonly Mock<IInputReaderBase> mockReader;

    public YesNoReaderAllowedValuesTests()
    {
        mockReader = new Mock<IInputReaderBase>();
    }

    [Test]
    [TestCase("y", ExpectedResult = true)]
    [TestCase("n", ExpectedResult = true)]
    public bool Read_WithAllowedValue_ShouldReturnExpectedIsValid(string input)
    {
        // Arrange
        ConfigureMockReader(input);

        var reader = BuildYesNoReader().WithAllowedValues('y', 'n');

        // Action
        var value = reader.Read();

        // return
        return value.IsValid;
    }

    [Test]
    [TestCase("y", false, ExpectedResult = "y")]
    [TestCase("n", false, ExpectedResult = "n")]
    [TestCase("Y", true, ExpectedResult = "Y")]
    [TestCase("N", true, ExpectedResult = "N")]
    public char? Read_WithParamsAllowedValue_ShouldReturnExpectedValue(string input, bool caseInSensitive)
    {
        // Arrange
        ConfigureMockReader(input);
        var reader = BuildYesNoReader().WithAllowedValues(caseInsensitive: caseInSensitive, null, 'y', 'n');

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }

    [Test]
    [TestCase("y", false, ExpectedResult = "y")]
    [TestCase("n", false, ExpectedResult = "n")]
    [TestCase("Y", true, ExpectedResult = "Y")]
    [TestCase("N", true, ExpectedResult = "N")]
    public char? Read_WithStringEnumerableAllowedValue_ShouldReturnExpectedValue(string input, bool caseInSensitive)
    {
        // Arrange
        ConfigureMockReader(input);
        IEnumerable<char?> enumerableValues = ['y', 'n'];
        var reader = BuildYesNoReader().WithAllowedValues(enumerableValues, caseInsensitive: caseInSensitive);

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }

    #region Private Methods

    private YesNoInputReader BuildYesNoReader()
    {
        return (YesNoInputReader)new YesNoInputReader()
                                    .ClearAllowedValues()
                                    .With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(string readLine)
    {
        mockReader.Setup(i => i.Read()).Returns(readLine);
    }

    #endregion
}
