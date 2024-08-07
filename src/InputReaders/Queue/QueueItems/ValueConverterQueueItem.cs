using InputReader.Converters;

namespace InputReader.InputReaders.Queue.QueueItems;

public sealed class ValueConverterQueueItem<TInputType> : IQueueItem, IHasFailReason
{
    private IValueConverter<TInputType> valueConverter;

    public ValueConverterQueueItem(IValueConverter<TInputType> valueConverter)
    {
        SetInputReader(valueConverter);
    }

    internal void SetInputReader(IValueConverter<TInputType> valueConverter)
    {
        this.valueConverter = valueConverter;
    }

    public int Order => QueueItemsOrder.ValueConverterQueueItem;

    public FailReason FailReason => FailReason.ValueConversion;

    public QueueItemResult Execute(QueueItemResult previousItemResult)
    {
        var message = previousItemResult.GetOutputParam(Constants.Queue.Params.Line);

        var success = valueConverter.TryConvert(message, out var value);

        if (success)
            previousItemResult.SetOutputParam(Constants.Queue.Params.ConvertedValue, value);

        return success
            ? QueueItemResult.FromResult(value, previousItemResult)
            : QueueItemResult.Failed();
    }
}