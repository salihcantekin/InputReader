using InputReader.Converters;
using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue.QueueItems;
using System;

namespace InputReader.InputReaders.BaseInputReaders;
public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private IValueConverter<TInputType> valueConverter;

    #region Converter Methods

    internal IInputReader<TInputType, TInputValueType> SetValueConverter(
        IValueConverter<TInputType> converter)
    {
        valueConverter = converter;

        var queueItem = GetOrCreateQueueItem(() => new ValueConverterQueueItem<TInputType>(valueConverter));

        queueItem.SetInputReader(converter);

        return this;
    }

    #endregion
}
