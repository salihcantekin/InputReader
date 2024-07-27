using System;
using System.Collections.Generic;
using System.Linq;

namespace InputReader.AllowedValues;

internal class DefaultAllowedValueManager<T> : IAllowedValueProcessor<T>
{
    private HashSet<T> allowedValuesHashSet;
    private IEqualityComparer<T> allowedValuesComparer;
    private bool isCaseInSensitive;

    public DefaultAllowedValueManager(IEqualityComparer<T> comparer = null)
    {
        SetEqualityComparer(comparer ?? EqualityComparer<T>.Default);
        allowedValuesHashSet ??= new(allowedValuesComparer);
    }

    public void ClearAllowedValues()
    {
        allowedValuesHashSet.Clear();
    }

    public IEnumerable<T> Values => allowedValuesHashSet.AsEnumerable();

    public bool IsEnabled => allowedValuesHashSet?.Count > 0;

    public bool IsCaseInSensitive => isCaseInSensitive;

    public void SetEqualityComparer(IEqualityComparer<T> comparer)
    {
        allowedValuesComparer = comparer;

        isCaseInSensitive = comparer == StringComparer.OrdinalIgnoreCase
                         || comparer == StringComparer.InvariantCultureIgnoreCase;

        // To be able to change the comparer
        if (allowedValuesHashSet is not null)
            allowedValuesHashSet = new(allowedValuesHashSet, allowedValuesComparer);
    }

    public bool IsAllowedValue(T value)
    {
        // No need to check the value
        if (allowedValuesHashSet.Count == 0)
            return true;

        return allowedValuesHashSet.Contains(value);
    }

    public bool AddAllowedValue(T value)
    {
        return allowedValuesHashSet.Add(value);
    }

    public void AddAllowedValues(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            _ = AddAllowedValue(value);
        }
    }


}