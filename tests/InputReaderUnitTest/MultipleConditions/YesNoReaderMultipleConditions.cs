using FluentAssertions;
using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.PrintProcessor;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputReaderUnitTest.MultipleConditions;
internal class YesNoReaderMultipleConditions
{
    private readonly Mock<IInputReaderBase> mockInputReader;
    private readonly Mock<IPrintProcessor> mockPrintProcessor;

    public YesNoReaderMultipleConditions()
    {
        mockInputReader = new();
        mockPrintProcessor = new();
    }


    [Test]
    public void Read_Case1()
    {
        // Arrange
        var expected = 'y';
        var line = "y";
        var errorMessage = "Invalid input. Please try again";
        ConfigureMockReader(line);
        ConfigureMockPrintProcessor();
        var reader = BuildReader();

        // Action
        var value = reader
                        .WithErrorMessage(errorMessage)
                        .Read();

        // Assert
        value.IsValid.Should().BeTrue();
        value.Value.Should().Be(expected);
        VerifyPrintErrorProcessor(Times.Never());
    }

    [Test]
    public void Read_Case2()
    {
        // Arrange
        var line = "1";
        var errorMessage = "Invalid input. Please try again";
        ConfigureMockReader(line);
        ConfigureMockPrintProcessor();
        var reader = BuildReader();

        // Action
        var value = reader
                        .WithErrorMessage(errorMessage)
                        .Read();

        // Assert
        value.IsValid.Should().BeFalse();
        value.Value.Should().BeNull();
        VerifyPrintErrorProcessor(Times.Once(), errorMessage);
    }



    #region Private methods

    private YesNoInputReader BuildReader(string message = null)
    {
        return (YesNoInputReader)new YesNoInputReader(message)
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
