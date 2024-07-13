using System.Collections.Generic;
using System.Collections.Immutable;

namespace InputReader.AllowedValues;

public interface IAllowedValueProcessor<T>
{
    bool IsEnabled { get; }
    bool IsCaseInSensitive { get; }

    IImmutableList<T> Values { get; }

    void SetEqualityComparer(IEqualityComparer<T> comparer);

    bool IsAllowedValue(T value);

    void ClearAllowedValues();

    bool AddAllowedValue(T value);
    void AddAllowedValues(IEnumerable<T> values);
}