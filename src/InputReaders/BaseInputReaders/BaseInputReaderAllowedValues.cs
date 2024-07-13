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
    internal IAllowedValueProcessor<string> AllowedValueProcessor;

    #region WithAllowedValues Methods

    public IInputReader<TInputType, TInputValueType> ClearAllowedValues()
    {
        AllowedValueProcessor?.ClearAllowedValues();
        return this;
    }

    public IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<string> allowedValues)
    {
        if (allowedValues is null)
            throw new ArgumentNullException(nameof(allowedValues));

        AllowedValueProcessor ??= new DefaultAllowedValueManager<string>();

        AllowedValueProcessor.AddAllowedValues(allowedValues);

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
            AllowedValueProcessor.SetEqualityComparer(StringComparer.OrdinalIgnoreCase);

        return this;
    }


    #region Internal Methods

    internal bool IsAllowedValuesEnabled()
    {
        return AllowedValueProcessor != null && AllowedValueProcessor.IsEnabled;
    }

    internal bool AllowedValuesCheckRequired()
    {
        return IsAllowedValuesEnabled() && AllowedValueProcessor.Values.Count > 0;
    }

    internal bool IsAllowedValue(string value)
    {
        return AllowedValueProcessor.IsAllowedValue(value);
    }

    #endregion

    #endregion
}