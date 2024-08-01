using System.Collections.Generic;

namespace InputReader.AllowedValues;

public interface IInRangeAllowedValueProcessor<TInputType>
{
    IEnumerable<(TInputType from, TInputType to)> InRangeValues { get; }

    bool AddAllowedValue(TInputType from, TInputType to);
    bool IsInRange(TInputType inputValueType);
}