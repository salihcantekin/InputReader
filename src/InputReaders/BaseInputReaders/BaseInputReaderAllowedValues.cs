using InputReader.AllowedValues;
using InputReader.InputReaders.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace InputReader.InputReaders.BaseInputReaders;

public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>, IPreValidatable<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private IAllowedValueProcessor<string> allowedValueProcessor;

    #region WithAllowedValues Methods

    public IInputReader<TInputType, TInputValueType> ClearAllowedValues()
    {
        allowedValueProcessor?.ClearAllowedValues();
        return this;
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<string> allowedValues)
    {
        if (allowedValues is null)
            throw new ArgumentNullException(nameof(allowedValues));

        allowedValueProcessor ??= new DefaultAllowedValueManager<string>();

        allowedValueProcessor.AddAllowedValues(allowedValues);

        var allowedValueQueueItem = new AllowedValuesCheckQueueItem(allowedValueProcessor);
        AddItemToQueue(allowedValueQueueItem);

        return this;
    }


    public IInputReader<TInputType, TInputValueType> WithAllowedValues(bool caseInsensitive = true, params TInputType[] allowedValues)
    {
        return WithAllowedValues(allowedValues.Select(i => i.ToString()), caseInsensitive);
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(params TInputType[] allowedValues)
    {
        return WithAllowedValues(allowedValues.Select(i => i.ToString()), false);
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<TInputType> allowedValues,
        bool caseInsensitive = true)
    {
        return WithAllowedValues(allowedValues.Select(i => i.ToString()), caseInsensitive);
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<string> allowedValues,
        bool caseInsensitive = true)
    {
        WithAllowedValues(allowedValues);

        if (caseInsensitive)
            allowedValueProcessor.SetEqualityComparer(StringComparer.OrdinalIgnoreCase);

        return this;
    }


    #region Internal Methods

    internal bool IsAllowedValuesEnabled()
    {
        return allowedValueProcessor != null && allowedValueProcessor.IsEnabled;
    }

    internal bool AllowedValuesCheckRequired()
    {
        return IsAllowedValuesEnabled() && allowedValueProcessor.Values.Count > 0;
    }

    internal bool IsAllowedValue(string value)
    {
        return allowedValueProcessor.IsAllowedValue(value);
    }

    #endregion

    #endregion
}