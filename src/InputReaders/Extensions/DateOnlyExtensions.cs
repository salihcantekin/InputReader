using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;
using System;
using InputReader.Converters.Constants;

namespace InputReader.InputReaders.Extensions;

public static class DateOnlyExtensions
{
    public static DateOnlyInputValue ReadUntilInRange(
        this IInputReader<CustomDateOnly?, DateOnlyInputValue> reader, string fromDate, string toDate, string format = RelatedConstant.Date)
    {
        DateOnlyValueConverter converter = new(format);
        if (!converter.TryConvertFromString(fromDate, out CustomDateOnly? fromDateValue))
        {
            throw new ArgumentException("Invalid fromDate value");
        }

        if (!converter.TryConvertFromString(toDate, out CustomDateOnly? toDateValue))
        {
            throw new ArgumentException("Invalid toDate value");
        }

        return reader.ReadUntil(input =>
        {
            return input.Value >= fromDateValue && input.Value <= toDateValue;
        });
    }
}
