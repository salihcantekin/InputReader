using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;
using System;
using InputReader.Converters.Constants;

namespace InputReader.InputReaders.Extensions;

public static class TimeOnlyExtensions
{
    public static TimeOnlyInputValue ReadUntilInRange(this IInputReader<CustomTimeOnly?, TimeOnlyInputValue> reader,
                                                      string fromTime,
                                                      string toTime,
                                                      string format = RelatedConstant.Time)
    {
        TimeOnlyValueConverter converter = new(format);
        if (!converter.TryConvertFromString(fromTime, out CustomTimeOnly? fromTimeValue))
        {
            throw new ArgumentException("Invalid fromTime value");
        }

        if (!converter.TryConvertFromString(toTime, out CustomTimeOnly? toTimeValue))
        {
            throw new ArgumentException("Invalid toTime value");
        }

        return reader.ReadUntil(input =>
        {
            return input.Value >= fromTimeValue && input.Value <= toTimeValue;
        });
    }
}
