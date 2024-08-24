using FluentAssertions;
using InputReader;
using InputReader.InputReaders;
using InputReader.InputReaders.BaseInputReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue;
using InputReader.InputReaders.Queue.QueueItems;
using Moq;

namespace InputReaderUnitTest.QueueItems;
internal class CreateInstanceQueueItemTests
{
    private CreateInstanceQueueItem queueItem;
    private readonly Mock<IInputReaderBase> mockReader;

    public CreateInstanceQueueItemTests()
    {
        mockReader = new Mock<IInputReaderBase>();
    }


    [Test]
    public void Execute_WithValidType_ShouldCreateInstanceOfExpectedType()
    {
        // Arrange
        var queueResult = GetDummyQueueItemResult(value: 10);
        Type instanceType = typeof(IntInputValue);
        ConfigureQueueItem(instanceType);

        // Action
        var result = queueItem.Execute(queueResult);

        // Assert
        result.Result.Should().BeOfType<IntInputValue>();
        result.IsFailed.Should().BeFalse();
    }


    [Test]
    public void Execute_WithValidType_ShouldCreateInstanceWithExpectedValue()
    {
        // Arrange
        var expectedValue = 10;
        var queueResult = GetDummyQueueItemResult(value: expectedValue);
        Type instanceType = typeof(IntInputValue);
        ConfigureQueueItem(instanceType);

        // Action
        var result = queueItem.Execute(queueResult);

        // Assert
        IntInputValue value = (IntInputValue)result.Result;
        value.Value.Should().Be(expectedValue);
        result.IsFailed.Should().BeFalse();
    }


    [Test]
    public void Execute_WithInValidType_ShouldReturnFailed()
    {
        // Arrange
        var queueResult = GetDummyQueueItemResult();
        Type instanceType = typeof(BaseInputReader<int?, IntInputValue>); // something that cannot be created
        ConfigureQueueItem(instanceType);

        // Action
        var result = queueItem.Execute(queueResult);

        // Assert
        result.IsFailed.Should().BeTrue();
    }




    #region Private Methods

    private IntInputReader BuildCharReader()
    {
        return (IntInputReader)new IntInputReader().With(builder => builder.WithConsoleReader(mockReader.Object));
    }


    private void ConfigureQueueItem(Type instanceType)
    {
        queueItem = new CreateInstanceQueueItem(instanceType);
    }

    private QueueItemResult GetDummyQueueItemResult(object? value = null, QueueItemResult previousResult = null)
    {
        if (previousResult is null)
            previousResult = QueueItemResult.FromResult(value, null);

        previousResult.SetOutputParam("ConvertedValue", value);

        return QueueItemResult.FromResult(value, previousResult);
    }

    #endregion
}
