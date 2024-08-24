using InputReader.InputValues.Comparers;
using System.Collections.Generic;
using System.Linq;

namespace InputReader.AllowedValues;

internal class DefaultAllowedValueManager<TInputType> : IAllowedValueProcessor<TInputType>
{
    private HashSet<TInputType> allowedValuesHashSet;
    private IEqualityComparer<TInputType> allowedValuesComparer;
    private bool isCaseInSensitive;
    private string errorMessage;
    private List<(TInputType, TInputType)> ranges;

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
    public IEnumerable<(TInputType from, TInputType to)> InRangeValues => ranges?.AsEnumerable() ?? [];

    public bool IsAllowedEnabled => allowedValuesHashSet?.Count > 0;
    public bool IsInRangeEnabled => ranges?.Count > 0;

    public bool IsCaseInSensitive => isCaseInSensitive;

    public string ErrorMessage => errorMessage;



    public void SetEqualityComparer(IEqualityComparer<TInputType> comparer)
    {
        allowedValuesComparer = comparer;

        isCaseInSensitive = comparer is ICaseInsensitiveComparer<TInputType>;

        // To be able to change the comparer
        if (allowedValuesHashSet is not null)
            allowedValuesHashSet = new(allowedValuesHashSet, comparer);
    }

    public void SetErrorMessage(string message)
    {
        errorMessage = message;
    }

    public virtual bool? IsAllowedValue(TInputType value)
    {
        // No need to check the value
        if (allowedValuesHashSet is null || allowedValuesHashSet.Count == 0)
            return null;

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

    public bool AddAllowedValue(TInputType from, TInputType to)
    {
        ranges ??= new(capacity: 5);

        ranges.Add((from, to));

        return true;
    }

    public bool? IsInRange(TInputType inputValueType)
    {
        if (ranges is not { Count: > 0 })
        {
            return null; // When ranges null or empty
        }

        foreach ((TInputType from, TInputType to) in ranges)
        {
            int comparisonFrom = Comparer<TInputType>.Default.Compare(inputValueType, from);
            int comparisonTo = Comparer<TInputType>.Default.Compare(inputValueType, to);

            if (comparisonFrom >= 0 && comparisonTo <= 0)
                return true;
        }

        return false;
    }

    #endregion
}