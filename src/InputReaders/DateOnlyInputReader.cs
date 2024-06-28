using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders;

public sealed class DateOnlyInputReader : BaseInputReader<CustomDateOnly, DateOnlyInputValue>
{
    public static DateOnlyInputReader DateOnly(string message = null, string format = "HH:mm:ss") => new(message, format);

    public DateOnlyInputReader(string message, string format = "HH:mm:ss") : base(message)
    {
        WithDateOnlyValueConverter(format);
    }

    public DateOnlyInputReader() : this(null)
    {
    }

    public IInputReader<CustomDateOnly, DateOnlyInputValue> WithDateOnlyValueConverter(string format = "yyyy-MM-dd")
    {
        return WithValueConverter(new DateOnlyValueConverter(format));
    }
}