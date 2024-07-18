using InputReader.Converters;
using InputReader.InputReaders.Queue;

namespace InputReader.InputReaders.Queue.QueueItems;

internal class ValueConverterQueueItem<TInputType>(IValueConverter<TInputType> valueConverter) : IQueueItem
{
    public int Order => 5;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var message = previousItemResult.GetOutputParam("line").ToString();

        var success = valueConverter.TryConvertFromString(message, out var value);

        return success
            ? QueueItemResult.FromResult(value, previousItemResult)
            : QueueItemResult.Failed();
    }
}