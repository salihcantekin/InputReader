using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders;

public sealed class DateOnlyInputReader : BaseInputReader<CustomDateOnly, DateOnlyInputValue>
{
    public static DateOnlyInputReader DateOnly(string message = null) => new(message);

    public DateOnlyInputReader(string message) : base(message)
    {
    }

    public DateOnlyInputReader()
    {
    }

    public IInputReader<CustomDateOnly, DateOnlyInputValue> WithDateOnlyValueConverter(string format = "yyyy-MM-dd")
    {
        WithValueConverter(new DateOnlyValueConverter(format));

        return this;
    }
}   