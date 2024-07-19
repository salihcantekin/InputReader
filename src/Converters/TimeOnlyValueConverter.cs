using InputReader.Converters.CustomConverters;

namespace InputReader.Converters;

public class TimeOnlyValueConverter(string format = Constants.Format.TimeFormat) : IValueConverter<CustomTimeOnly?>
{
    public bool TryConvertFromString(string consoleInput, out CustomTimeOnly? value)
    {
        return CustomTimeOnly.TryParseExact(consoleInput, format, out value);
    }
}