using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;
using System;

namespace InputReader.InputReaders.Extensions;

public static class DateOnlyExtensions
{
    public static DateOnlyInputValue ReadUntilInRange(
        this IInputReader<CustomDateOnly?, DateOnlyInputValue> reader, string fromDate, string toDate, string format = Constants.Format.DateFormat)
    {
        DateOnlyValueConverter converter = new(format);
        if (!converter.TryConvert(fromDate, out CustomDateOnly? fromDateValue))
        {
            throw new ArgumentException(Constants.Message.InvalidValueFormat.Format(nameof(fromDate)));
        }

        if (!converter.TryConvert(toDate, out CustomDateOnly? toDateValue))
        {
            throw new ArgumentException(Constants.Message.InvalidValueFormat.Format(nameof(toDate)));
        }

        return reader.ReadUntil(input =>
        {
            return input.Value >= fromDateValue && input.Value <= toDateValue;
        });
    }
}
