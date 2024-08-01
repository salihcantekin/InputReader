using InputReader.Converters;
using InputReader.InputReaders.Queue;
using InputReader.InputReaders.Queue.QueueItems;

namespace InputReaderUnitTest.InputReaders;
internal class InputReadersTests
{
    public InputReadersTests()
    {

    }

    [Test]
    public void Execute_WithValidInt_ShouldReturnInt()
    {
        // Arrange 
        var expectedValue = 10;
        var defaultConverter = new DefaultValueConverter<int>(message =>
        {
            return expectedValue;
        });

        var converterItem = new ValueConverterQueueItem<int>(defaultConverter);

        var previousResult = QueueItemResult.FromResult(1, null);
        previousResult.AddOutputParam("Line", expectedValue.ToString());

        // Action
        var result = converterItem.Execute(previousResult);

        // Assert
        Assert.That((int)result.Result, Is.EqualTo(expectedValue));
        Assert.That(result.IsFailed, Is.False);
    }

    [Test]
    public void Execute_WithInvalidInt_ShouldReturnFailed()
    {
        // Arrange 
        var defaultConverter = new DefaultValueConverter<int>();

        var converterItem = new ValueConverterQueueItem<int>(defaultConverter);

        var previousResult = QueueItemResult.FromResult(1, null);
        previousResult.AddOutputParam("Line", "Invalid");

        // Action
        var result = converterItem.Execute(previousResult);

        // Assert
        Assert.That(result.IsFailed, Is.True);
    }

    [Test]
    public void Execute_WithValidString_ShouldReturnString()
    {
        // Arrange 
        var expectedValue = "Test";
        var defaultConverter = new DefaultValueConverter<string>(message =>
        {
            return expectedValue;
        });

        var converterItem = new ValueConverterQueueItem<string>(defaultConverter);

        var previousResult = QueueItemResult.FromResult(1, null);
        previousResult.AddOutputParam("Line", expectedValue);

        // Action
        var result = converterItem.Execute(previousResult);

        // Assert
        Assert.That((string)result.Result, Is.EqualTo(expectedValue));
        Assert.That(result.IsFailed, Is.False);
    }
}
