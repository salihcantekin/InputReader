using InputReader.AllowedValues;
using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue.QueueItems;
using InputReader.InputValues.Comparers;
using System;
using System.Collections.Generic;

namespace InputReader.InputReaders.BaseInputReaders;

public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private IAllowedValueProcessor<TInputType> allowedValueProcessor;

    #region In Range AllowedValues

    internal IInputReader<TInputType, TInputValueType> WithInRangeAllowedValues(TInputType from, TInputType to)
    {
        EnsureAllowedValueProcessor();

        allowedValueProcessor.AddAllowedValue(from, to);

        UpdateProcessor();

        return this;
    }

    #endregion

    #region WithAllowedValues Methods

    public IInputReader<TInputType, TInputValueType> ClearAllowedValues()
    {
        allowedValueProcessor?.ResetAllAllowedValues();
        return this;
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<TInputType> allowedValues, string errorMessage = null)
    {
        return WithAllowedValues(allowedValues, caseInsensitive: false, errorMessage);
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<TInputType> allowedValues, bool caseInsensitive)
    {
        return WithAllowedValues(allowedValues, caseInsensitive: caseInsensitive, null);
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(bool caseInsensitive = true, string errorMessage = null, params TInputType[] allowedValues)
    {
        return WithAllowedValues(allowedValues, caseInsensitive, errorMessage);
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(params TInputType[] allowedValues)
    {
        return WithAllowedValues(allowedValues, false, null);
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<TInputType> allowedValues,
        bool caseInsensitive, string errorMessage)
    {
        _ = allowedValues ?? throw new ArgumentNullException(nameof(allowedValues)); // Compact null check
        EnsureAllowedValueProcessor();

        if (caseInsensitive) // Conditionally set the comparer
        {
            allowedValueProcessor.SetEqualityComparer(GetComparer());
        }

        allowedValueProcessor.AddAllowedValues(allowedValues);

        if (errorMessage is not null) // Set error message only if provided
        {
            allowedValueProcessor.SetErrorMessage(errorMessage);
        }

        UpdateProcessor();

        return this;
    }

    private IEqualityComparer<TInputType> GetComparer()
    {
        //Case - insensitive karşılaştırmaları destekleyen tipler
        var underlyingType = Nullable.GetUnderlyingType(typeof(TInputType));

        if (underlyingType == typeof(string) || typeof(TInputType) == typeof(string) || underlyingType == typeof(char))
            return new CaseInSensitiveComparer<TInputType>();

        return EqualityComparer<TInputType>.Default;
    }


    #endregion

    #region Private Methods

    private void UpdateProcessor()
    {
        AddItemToQueue(new AllowedValuesCheckQueueItem<TInputType>(allowedValueProcessor, PrintProcessor));

        var printQueueItem = TryGetQueueItem<ProcessPrintQueueItem<TInputType>>();

        printQueueItem?.SetAllowedValueProcessor(allowedValueProcessor);
    }

    private void EnsureAllowedValueProcessor()
    {
        allowedValueProcessor ??= new DefaultAllowedValueManager<TInputType>();
    }

    #endregion
}