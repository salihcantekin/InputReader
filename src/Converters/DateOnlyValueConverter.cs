using InputReader.Converters.CustomConverters;

namespace InputReader.Converters;

public class DateOnlyValueConverter(string format = Constants.Format.DateFormat) : IValueConverter<CustomDateOnly?>
{
    public bool TryConvert(object consoleInput, out CustomDateOnly? value)
    {
        return CustomDateOnly.TryParseExact(consoleInput.ToString(), format, out value);
    }
}
