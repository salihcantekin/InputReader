using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using Moq;

namespace InputReaderUnitTest.AllowedValues;
internal class StringReaderAllowedValuesTests
{
    private readonly Mock<IInputReaderBase> mockReader;

    public StringReaderAllowedValuesTests()
    {
        mockReader = new Mock<IInputReaderBase>();
    }

    [Test]
    [TestCase("tech", ExpectedResult = true)]
    [TestCase("buddy", ExpectedResult = false)]
    public bool Read_WithAllowedValue_ShouldReturnExpectedIsValid(string input)
    {
        // Arrange
        ConfigureMockReader(input);

        var reader = BuildStringReader().WithAllowedValues("tech", "techbuddy");

        // Action
        var value = reader.Read();

        // return
        return value.IsValid;
    }

    [Test]
    [TestCase("tech", false, ExpectedResult = "tech")]
    [TestCase("techbuddy", false, ExpectedResult = "techbuddy")]
    [TestCase("buddy", false, ExpectedResult = null)]
    [TestCase("Tech", true, ExpectedResult = "Tech")]
    [TestCase("TechBuddy", true, ExpectedResult = "TechBuddy")]
    public string? Read_WithParamsAllowedValue_ShouldReturnExpectedValue(string input, bool caseInSensitive)
    {
        // Arrange
        ConfigureMockReader(input);
        var reader = BuildStringReader().WithAllowedValues(caseInsensitive: caseInSensitive, null, "tech", "techbuddy");

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }

    [Test]
    [TestCase("tech", false, ExpectedResult = "tech")]
    [TestCase("techbuddy", false, ExpectedResult = "techbuddy")]
    [TestCase("buddy", false, ExpectedResult = null)]
    [TestCase("Tech", true, ExpectedResult = "Tech")]
    [TestCase("TechBuddy", true, ExpectedResult = "TechBuddy")]
    public string? Read_WithStringEnumerableAllowedValue_ShouldReturnExpectedValue(string input, bool caseInSensitive)
    {
        // Arrange
        ConfigureMockReader(input);
        IEnumerable<string> enumerableValues = ["tech", "techbuddy"];
        var reader = BuildStringReader().WithAllowedValues(enumerableValues, caseInsensitive: caseInSensitive);

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }

    #region Private Methods

    private StringInputReader BuildStringReader()
    {
        return (StringInputReader)new StringInputReader().With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(string readLine)
    {
        mockReader.Setup(i => i.ReadLine()).Returns(readLine);
    }

    #endregion
}
