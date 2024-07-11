using InputReader.Converters.CustomConverters;
using System;
using System.Globalization;

namespace InputReader.Converters;

public class DateOnlyValueConverter(string format = "yyyy-MM-dd") : IValueConverter<CustomDateOnly?>
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