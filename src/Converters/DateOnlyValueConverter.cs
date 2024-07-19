using InputReader.Converters.CustomConverters;

namespace InputReader.Converters;

public class DateOnlyValueConverter(string format = Constants.Format.DateFormat) : IValueConverter<CustomDateOnly?>
{
    public bool TryConvertFromString(string consoleInput, out CustomDateOnly? value)
    {
        return CustomDateOnly.TryParseExact(consoleInput, format, out value);
    }
}