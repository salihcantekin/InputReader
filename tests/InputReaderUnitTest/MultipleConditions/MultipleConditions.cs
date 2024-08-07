using FluentAssertions;
using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.PrintProcessor;
using Moq;

namespace InputReaderUnitTest.MultipleConditions;
internal class MultipleConditions
{
    private readonly Mock<IInputReaderBase> mockInputReader;
    private readonly Mock<IPrintProcessor> mockPrintProcessor;

    public MultipleConditions()
    {
        mockInputReader = new();
        mockPrintProcessor = new();
    }


    [Test]
    public void Read_Case1()
    {
        // Arrange
        var line = "1";
        ConfigureMockReader(line);
        ConfigureMockPrintProcessor();
        var reader = BuildIntReader();

        // Action
        var value = reader
                        .WithAllowedValues(1, 2, 3)
                        .WithErrorMessage("Invalid input. Please try again")
                        .Read();

        // Assert
        value.IsValid.Should().BeTrue();
        value.Value.Should().Be(1);
        VerifyPrintErrorProcessor(Times.Never());
    }

    [Test]
    public void Read_Case2()
    {
        // Arrange
        var line = "1";
        var inputMessage = "Enter a number: ";
        var errorMessage = "Invalid input. Please try again";
        ConfigureMockReader(line);
        ConfigureMockPrintProcessor();
        var reader = BuildIntReader(inputMessage);

        // Action
        var value = reader
                        .WithAllowedValues([2, 3])
                        .WithErrorMessage(errorMessage)
                        .Read();

        // Assert
        value.IsValid.Should().BeFalse();
        value.FailReason.Should().Be(FailReason.AllowedValues);
        VerifyPrintErrorProcessor(Times.Once(), errorMessage);
        VerifyPrintProcessor(Times.Once(), inputMessage);
    }


    [Test]
    public void Read_Case3()
    {
        // Arrange
        var line = "A1";
        var inputMessage = "Enter a number: ";
        var errorMessage = "Invalid input. Please try again";
        int maxTry = 3;
        int expectedErrorCount = maxTry + 1;

        ConfigureMockReader(line);
        ConfigureMockPrintProcessor();
        var reader = BuildIntReader(inputMessage);

        // Action
        var value = reader
                        .WithIteration((result, printProcessor) =>
                        {
                            printProcessor.PrintError(errorMessage);
                        })
                        .ReadUntilValid(maxTry);

        // Assert
        value.IsValid.Should().BeFalse();
        value.FailReason.Should().Be(FailReason.PreValidation);
        VerifyPrintErrorProcessor(Times.AtLeast(expectedErrorCount), errorMessage);
    }



    #region Private Methods

    private IntInputReader BuildIntReader(string message = null)
    {
        return (IntInputReader)new IntInputReader(message)
                    .With(builder =>
                    {
                        builder.WithConsoleReader(mockInputReader.Object);
                        builder.WithPrintProcessor(mockPrintProcessor.Object);
                    });
    }

    private void ConfigureMockReader(string readLine)
    {
        mockInputReader.Setup(i => i.Read()).Returns(readLine);
    }

    private void ConfigureMockPrintProcessor()
    {
        mockPrintProcessor.Setup(i => i.PrintError(It.IsAny<string>()));
    }

    private void VerifyPrintErrorProcessor(Times times, string message = null)
    {
        mockPrintProcessor.Verify(i => i.PrintError(message ?? It.IsAny<string>()), times);
    }

    private void VerifyPrintProcessor(Times times, string message = null)
    {
        mockPrintProcessor.Verify(i => i.Print(message ?? It.IsAny<string>()), times);
    }

    #endregion


}
