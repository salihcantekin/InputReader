using InputReader.InputReaders.Interfaces;
using System;
using System.Collections.Generic;

namespace InputReader.AllowedValues;

internal class DefaultInRangeAllowedValueManager<TInputType> : IInRangeAllowedValueProcessor<TInputType>
{
    private readonly List<(TInputType, TInputType)> ranges;

    public DefaultInRangeAllowedValueManager()
    {
        ranges = [];
    }

    public IEnumerable<(TInputType from, TInputType to)> Values => ranges;

    public bool AddAllowedValue(TInputType from, TInputType to)
    {
        ranges.Add((from, to));
        return true;
    }

    public bool IsInRange(TInputType inputValueType)
    {
        if (ranges?.Count == 0)
            return true;

        var result = false;

        foreach ((TInputType from, TInputType to) in ranges)
        {
            bool inRange = IInRangeCompatible<TInputType>.IsInRange(inputValueType, from, to);
            if (inRange)
            {
                return true;
            }
        }

        return result;
    }
}
