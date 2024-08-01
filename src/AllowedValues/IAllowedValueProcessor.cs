using System.Collections.Generic;

namespace InputReader.AllowedValues;

internal interface IAllowedValueProcessor<TRawValueType, TInRangeInputType> : IInRangeAllowedValueProcessor<TInRangeInputType>
{
    bool IsAllowedEnabled { get; }
    bool IsInRangeEnabled { get; }
    bool IsCaseInSensitive { get; }

    string ErrorMessage { get; }

    IEnumerable<TRawValueType> AllowedValues { get; }

    void SetEqualityComparer(IEqualityComparer<TRawValueType> comparer);

    bool IsAllowedValue(TRawValueType value);

    void ResetAllAllowedValues();

    bool AddAllowedValue(TRawValueType value);
    void AddAllowedValues(IEnumerable<TRawValueType> values);

    void SetErrorMessage(string message);
}