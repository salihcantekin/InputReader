using FluentAssertions;
using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using Moq;

namespace InputReaderUnitTest.ExtensionTests;

[TestFixture]
public class ReadUntilValidEmailTests
{
    private Mock<IInputReaderBase> mockReader;

    [SetUp]
    public void Setup()
    {
        mockReader = new Mock<IInputReaderBase>();
    }


    [Test]
    [TestCase("salihcantekin@gmail.com", ExpectedResult = true)]
    [TestCase("test@test.com", ExpectedResult = true)]
    public bool ReadUntilValidEmail_WithValidEmail_ShouldReturnValid(string input)
    {
        // Arrange
        ConfigureMockReader(input);

        // Action
        var emailValue = BuildStringReader().ReadUntilValidEmail();

        // Assert
        emailValue.Should().NotBeNull();
        
        return emailValue.IsValid;
    }



    #region Private Methods

    private StringInputReader BuildStringReader()
    {
        return (StringInputReader)new StringInputReader().With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(string readLine)
    {
        mockReader.Setup(i => i.Read()).Returns(readLine);
    }

    #endregion
}
