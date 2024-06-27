using System;
using System.Globalization;
using InputReader.Converters.CustomConverters;

namespace InputReader.Converters;

public class TimeOnlyValueConverter(string format = "HH:mm:ss") : IValueConverter<CustomTimeOnly>
{
    public bool TryConvertFromString(string consoleInput, out CustomTimeOnly value)
    {
        try
        {
            return CustomTimeOnly.TryParseExact(consoleInput, format, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out value);
        }
        catch (ArgumentException)
        {
            value = default;
            return false;
        }
    }
}