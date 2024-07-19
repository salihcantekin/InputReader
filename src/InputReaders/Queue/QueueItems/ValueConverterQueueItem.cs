using InputReader.Converters;

namespace InputReader.InputReaders.Queue.QueueItems;

public sealed class ValueConverterQueueItem<TInputType>(IValueConverter<TInputType> valueConverter) : IQueueItem
{
    public int Order => 5;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var message = previousItemResult.GetOutputParam("line").ToString();

        var success = valueConverter.TryConvertFromString(message, out var value);

        if (success)
            previousItemResult.AddOutputParam("converted_value", value);

        return success
            ? QueueItemResult.FromResult(value, previousItemResult)
            : QueueItemResult.Failed();
    }
}