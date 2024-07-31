using System.Collections.Generic;

namespace InputReader.AllowedValues;

public interface IAllowedValueProcessor<T>
{
    bool IsEnabled { get; }
    bool IsCaseInSensitive { get; }

    string ErrorMessage { get; }

    IEnumerable<T> Values { get; }

    void SetEqualityComparer(IEqualityComparer<T> comparer);

    bool IsAllowedValue(T value);

    void ClearAllowedValues();

    bool AddAllowedValue(T value);
    void AddAllowedValues(IEnumerable<T> values);

    void SetErrorMessage(string message);
}