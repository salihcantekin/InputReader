using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;
using System;

namespace InputReader.InputReaders.Extensions;

public static class TimeOnlyExtensions
{
    public static TimeOnlyInputValue ReadUntilInRange(this IInputReader<CustomTimeOnly?, TimeOnlyInputValue> reader,
                                                      string fromTime,
                                                      string toTime,
                                                      string format = Constants.Format.TimeFormat)
    {
        TimeOnlyValueConverter converter = new(format);
        if (!converter.TryConvert(fromTime, out CustomTimeOnly? fromTimeValue))
        {
            throw new ArgumentException(Constants.Message.InvalidValueFormat.Format(nameof(fromTime)));
        }

        if (!converter.TryConvert(toTime, out CustomTimeOnly? toTimeValue))
        {
            throw new ArgumentException(Constants.Message.InvalidValueFormat.Format(nameof(toTime)));
        }

        return reader.ReadUntil(input =>
        {
            return input.Value >= fromTimeValue && input.Value <= toTimeValue;
        });
    }
}
