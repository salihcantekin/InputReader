using System.Collections.Generic;

namespace InputReader.AllowedValues;

internal interface IAllowedValueProcessor<TInputType>
{
    bool IsAllowedEnabled { get; }
    bool IsInRangeEnabled { get; }
    bool IsCaseInSensitive { get; }

    string ErrorMessage { get; }

    IEnumerable<TInputType> AllowedValues { get; }

    void SetEqualityComparer(IEqualityComparer<TInputType> comparer);

    bool? IsAllowedValue(TInputType value);

    bool? IsInRange(TInputType inputValueType);

    void ResetAllAllowedValues();

    bool AddAllowedValue(TInputType value);
    void AddAllowedValues(IEnumerable<TInputType> values);

    void SetErrorMessage(string message);

    IEnumerable<(TInputType from, TInputType to)> InRangeValues { get; }

    bool AddAllowedValue(TInputType from, TInputType to);

}