﻿using InputReader.Converters;
using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders.Queue.QueueItems;

public sealed class ValueConverterQueueItem<TInputType> : IQueueItem
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