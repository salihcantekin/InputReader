using InputReader.InputReaders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InputReader.AllowedValues;

internal class DefaultAllowedValueManager<TInputType, TInRangeInputValue> : IAllowedValueProcessor<TInputType, TInRangeInputValue>
{
    private HashSet<TInputType> allowedValuesHashSet;
    private IEqualityComparer<TInputType> allowedValuesComparer;
    private bool isCaseInSensitive;
    private string errorMessage;
    private List<(TInRangeInputValue, TInRangeInputValue)> ranges;

    public DefaultAllowedValueManager(IEqualityComparer<TInputType> comparer = null)
    {
        SetEqualityComparer(comparer ?? EqualityComparer<TInputType>.Default);
        allowedValuesHashSet ??= new(allowedValuesComparer);
    }

    public void ResetAllAllowedValues()
    {
        allowedValuesHashSet.Clear();
        ranges?.Clear();
    }

    public IEnumerable<TInputType> AllowedValues => allowedValuesHashSet.AsEnumerable();
    public IEnumerable<(TInRangeInputValue from, TInRangeInputValue to)> InRangeValues => ranges?.AsEnumerable() ?? [];

    public bool IsAllowedEnabled => allowedValuesHashSet?.Count > 0;
    public bool IsInRangeEnabled => ranges?.Count > 0;

    public bool IsCaseInSensitive => isCaseInSensitive;

    public string ErrorMessage => errorMessage;



    public void SetEqualityComparer(IEqualityComparer<TInputType> comparer)
    {
        allowedValuesComparer = comparer;

        isCaseInSensitive = comparer == StringComparer.OrdinalIgnoreCase
                         || comparer == StringComparer.InvariantCultureIgnoreCase;

        // To be able to change the comparer
        if (allowedValuesHashSet is not null)
            allowedValuesHashSet = new(allowedValuesHashSet, allowedValuesComparer);
    }

    public void SetErrorMessage(string message)
    {
        errorMessage = message;
    }

    public virtual bool IsAllowedValue(TInputType value)
    {
        // No need to check the value
        if (allowedValuesHashSet.Count == 0)
            return true;

        return allowedValuesHashSet.Contains(value);
    }

    public bool AddAllowedValue(TInputType value)
    {
        return allowedValuesHashSet.Add(value);
    }

    public void AddAllowedValues(IEnumerable<TInputType> values)
    {
        foreach (var value in values)
        {
            _ = AddAllowedValue(value);
        }
    }

    #region InRange Allowed Values Methods

    public bool AddAllowedValue(TInRangeInputValue from, TInRangeInputValue to)
    {
        ranges ??= new(capacity: 5);

        ranges.Add((from, to));

        return true;
    }

    public bool IsInRange(TInRangeInputValue inputValueType)
    {
        if (ranges?.Count == 0)
            return true;

        var result = false;

        foreach ((TInRangeInputValue from, TInRangeInputValue to) in ranges)
        {
            bool inRange = IInRangeCompatible<TInRangeInputValue>.IsInRange(inputValueType, from, to);
            if (inRange)
            {
                return true;
            }
        }

        return result;
    }

    #endregion
}