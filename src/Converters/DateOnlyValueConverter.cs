using InputReader.Converters.CustomConverters;
using System;
using System.Globalization;
using InputReader.Converters.Constants;

namespace InputReader.Converters;

public class DateOnlyValueConverter(string format = RelatedConstant.Date) : IValueConverter<CustomDateOnly?>
{
    public bool TryConvertFromString(string consoleInput, out CustomDateOnly? value)
    {
        try
        {
            return CustomDateOnly.TryParseExact(consoleInput, format, out value);
        }
        catch (ArgumentException)
        {
            value = default;
            return false;
        }
    }
}