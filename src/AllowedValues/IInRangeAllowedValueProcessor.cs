using System.Collections.Generic;

namespace InputReader.AllowedValues;

internal interface IInRangeAllowedValueProcessor<TInputType>
{
    IEnumerable<(TInputType from, TInputType to)> Values { get; }

    bool AddAllowedValue(TInputType from, TInputType to);
    bool IsInRange(TInputType inputValueType);
}