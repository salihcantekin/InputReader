using FluentAssertions;
using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue;
using InputReader.InputReaders.Queue.QueueItems;
using Moq;

namespace InputReaderUnitTest.QueueItems;
internal class ConsoleReadLineQueueItemTests
{
    private ConsoleReadQueueItem queueItem;
    private readonly Mock<IInputReaderBase> mockReader;

    public ConsoleReadLineQueueItemTests()
    {
        mockReader = new Mock<IInputReaderBase>();
    }

    [Test]
    public void Execute_ShouldCallReader()
    {
        // Arrange
        ConfigureMockReader();
        ConfigureQueueItem();
        var queueResult = GetDummyQueueItemResult();

        // Action
        queueItem.Execute(queueResult);

        // Assert
        mockReader.Verify(i => i.Read(), Times.Once);
    }

    [Test]
    public void Execute_ShouldReturnNotNullResult()
    {
        // Arrange
        ConfigureMockReader();
        ConfigureQueueItem();
        var queueResult = GetDummyQueueItemResult();

        // Action
        var queueItemResult = queueItem.Execute(queueResult);

        // Assert
        queueItemResult.Should().NotBeNull();
    }


    [Test]
    public void Execute_ShouldSetPreviousResult()
    {
        // Arrange
        ConfigureMockReader();
        ConfigureQueueItem();
        var queueResult = GetDummyQueueItemResult();

        // Action
        var newResult = queueItem.Execute(queueResult);

        // Assert
        queueResult.Should().BeEquivalentTo(newResult.PreviousItemResult);
    }


    [Test]
    public void Execute_ShouldSetLineAsResultParam()
    {
        // Arrange
        var input = "console_input";
        ConfigureMockReader(input);
        ConfigureQueueItem();
        var queueResult = GetDummyQueueItemResult();

        // Action
        var newResult = queueItem.Execute(queueResult);
        var param = newResult.GetOutputParam("Line");

        // Assert
        param.Should().NotBeNull();
        param.Should().Be(input);
    }



    #region Private Methods

    private IntInputReader BuildCharReader()
    {
        return (IntInputReader)new IntInputReader().With(builder => builder.WithConsoleReader(mockReader.Object));
    }

    private void ConfigureMockReader(string readLine = "")
    {
        mockReader.Setup(i => i.Read()).Returns(readLine);
    }

    private void ConfigureQueueItem()
    {
        queueItem = new ConsoleReadQueueItem(mockReader.Object);
    }

    private QueueItemResult GetDummyQueueItemResult(object value = null, QueueItemResult previousResult = null) => QueueItemResult.FromResult(value, previousResult);

    #endregion
}
