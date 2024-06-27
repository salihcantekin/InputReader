using System;
using System.Globalization;
using InputReader.Converters.CustomConverters;

namespace InputReader.Converters;

public class DateOnlyValueConverter(string format = "yyyy-MM-dd") : IValueConverter<CustomDateOnly>
{
    public bool TryConvertFromString(string consoleInput, out CustomDateOnly value)
    {
        try
        {
            return CustomDateOnly.TryParseExact(consoleInput, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out value);
        }
        catch (ArgumentException)
        {
            value = default;
            return false;
        }
    }
}