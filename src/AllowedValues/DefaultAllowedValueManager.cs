using System;
using System.Collections.Generic;
using System.Linq;

namespace InputReader.AllowedValues;

internal class DefaultAllowedValueManager<TInputType> : IAllowedValueProcessor<TInputType>
{
    private HashSet<TInputType> allowedValuesHashSet;
    private IEqualityComparer<TInputType> allowedValuesComparer;
    private bool isCaseInSensitive;
    private string errorMessage;

    public DefaultAllowedValueManager(IEqualityComparer<TInputType> comparer = null)
    {
        SetEqualityComparer(comparer ?? EqualityComparer<TInputType>.Default);
        allowedValuesHashSet ??= new(allowedValuesComparer);
    }

    public void ClearAllowedValues()
    {
        allowedValuesHashSet.Clear();
    }

    public IEnumerable<TInputType> Values => allowedValuesHashSet.AsEnumerable();

    public bool IsEnabled => allowedValuesHashSet?.Count > 0;

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


}