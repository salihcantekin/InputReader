using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using Moq;

namespace InputReaderUnitTest.AllowedValues;
internal class CharReaderAllowedValuesTests
{
    private readonly Mock<IInputReaderBase> mockReader;

    public CharReaderAllowedValuesTests()
    {
        mockReader = new Mock<IInputReaderBase>();
    }

    [Test]
    [TestCase('a', ExpectedResult = true)]
    [TestCase('b', ExpectedResult = false)]
    public bool Read_WithAllowedValue_ShouldReturnExpectedIsValid(char input)
    {
        // Arrange
        ConfigureMockReader(input);

        var reader = BuildCharReader().WithAllowedValues('a', 'c');

        // Action
        var value = reader.Read();

        // return
        return value.IsValid;
    }

    [Test]
    [TestCase('a', false, ExpectedResult = 'a')]
    [TestCase('c', false, ExpectedResult = 'c')]
    [TestCase('b', false, ExpectedResult = null)]
    [TestCase('A', true, ExpectedResult = 'A')]
    [TestCase('C', true, ExpectedResult = 'C')]
    public char? Read_WithParamsAllowedValue_ShouldReturnExpectedValue(char input, bool caseInSensitive)
    {
        // Arrange
        ConfigureMockReader(input);
        var reader = BuildCharReader().WithAllowedValues(caseInsensitive: caseInSensitive, null, 'a', 'c');

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }

    [Test]
    [TestCase('a', false, ExpectedResult = 'a')]
    [TestCase('c', false, ExpectedResult = 'c')]
    [TestCase('b', false, ExpectedResult = null)]
    [TestCase('A', true, ExpectedResult = 'A')]
    [TestCase('C', true, ExpectedResult = 'C')]
    public char? Read_WithCharEnumerableAllowedValue_ShouldReturnExpectedValue(char input, bool caseInSensitive)
    {
        // Arrange
        ConfigureMockReader(input);
        IEnumerable<char?> enumerableValues = ['a', 'c'];
        var reader = BuildCharReader().WithAllowedValues(enumerableValues, caseInsensitive: caseInSensitive);

        // Action
        var value = reader.Read();

        // return
        return value.Value;
    }

    #region Private Methods

    private CharInputReader BuildCharReader()
    {
        return (CharInputReader)new CharInputReader().With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(char readLine)
    {
        mockReader.Setup(i => i.ReadLine()).Returns(readLine.ToString());
    }

    #endregion
}
