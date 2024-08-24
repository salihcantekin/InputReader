using InputReader.Converters.CustomConverters;

namespace InputReader.Converters;

public class TimeOnlyValueConverter(string format = Constants.Format.TimeFormat) : IValueConverter<CustomTimeOnly?>
{
    public bool TryConvert(object consoleInput, out CustomTimeOnly? value)
    {
        return CustomTimeOnly.TryParseExact(consoleInput.ToString(), format, out value);
    }
}