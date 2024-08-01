using InputReader.AllowedValues;
using InputReader.Converters;
using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue.QueueItems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InputReader.InputReaders.BaseInputReaders;

public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private IAllowedValueProcessor<string, TInputType> allowedValueProcessor;

    #region In Range AllowedValues

    internal IInputReader<TInputType, TInputValueType> WithInRangeAllowedValues(TInputType from, TInputType to)
    {
        allowedValueProcessor.AddAllowedValue(from, to);

        SetInRangeAllowedValueManager(allowedValueProcessor);

        return this;
    }

    #endregion

    #region WithAllowedValues Methods

    public IInputReader<TInputType, TInputValueType> ClearAllowedValues()
    {
        allowedValueProcessor?.ResetAllAllowedValues();
        return this;
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<string> allowedValues,
        string errorMessage = null)
    {
        if (allowedValues is null)
            throw new ArgumentNullException(nameof(allowedValues));

        allowedValueProcessor ??= new DefaultAllowedValueManager<string, TInputType>();

        allowedValueProcessor.AddAllowedValues(allowedValues);

        if (!string.IsNullOrWhiteSpace(errorMessage))
            allowedValueProcessor.SetErrorMessage(errorMessage);

        AddItemToQueue(new AllowedValuesCheckQueueItem<TInputType>(allowedValueProcessor, PrintProcessor));

        var printQueueItem = TryGetQueueItem<ProcessPrintQueueItem<TInputType>>();
        
        printQueueItem?.SetAllowedValueProcessor(allowedValueProcessor);

        return this;
    }


    public IInputReader<TInputType, TInputValueType> WithAllowedValues(bool caseInsensitive = true, string errorMessage = null, params TInputType[] allowedValues)
    {
        return WithAllowedValues(allowedValues.Select(i => i.ToString()), caseInsensitive, errorMessage);
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(params TInputType[] allowedValues)
    {
        return WithAllowedValues(allowedValues.Select(i => i.ToString()), false, null);
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<TInputType> allowedValues,
        bool caseInsensitive = true, string errorMessage = null)
    {
        return WithAllowedValues(allowedValues.Select(i => i.ToString()), caseInsensitive, errorMessage);
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<string> allowedValues,
        bool caseInsensitive = true, string errorMessage = null)
    {
        WithAllowedValues(allowedValues, errorMessage);

        if (caseInsensitive)
            allowedValueProcessor.SetEqualityComparer(StringComparer.OrdinalIgnoreCase);

        return this;
    }

    #endregion

    internal IInputReader<TInputType, TInputValueType> SetInRangeAllowedValueManager(IInRangeAllowedValueProcessor<TInputType> inRangeAllowedValueManager)
    {
        var queueItem = GetOrCreateQueueItem(() =>
        {
            // first time
            var item = new InRangeAllowedValuesQeueItem<TInputType>();
            item.SetManager(allowedValueProcessor);
            return item;
        });

        queueItem.SetManager(allowedValueProcessor);

        var printQueueItem = TryGetQueueItem<ProcessPrintQueueItem<TInputType>>();
        
        printQueueItem?.SetAllowedValueProcessor(allowedValueProcessor);

        return this;
    }
}