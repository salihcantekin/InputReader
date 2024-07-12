using InputReader.Converters;
using InputReader.Converters.Constants;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders;

public sealed class DateOnlyInputReader : BaseInputReader<CustomDateOnly?, DateOnlyInputValue>
{
    public static DateOnlyInputReader DateOnly(string message = null, string format = RelatedConstant.Date) => new(message, format);

    public DateOnlyInputReader(string message, string format = RelatedConstant.Date) : base(message)
    {
        WithDateOnlyValueConverter(format);
    }

    public DateOnlyInputReader() : this(null)
    {
    }

    public IInputReader<CustomDateOnly?, DateOnlyInputValue> WithDateOnlyValueConverter(string format = RelatedConstant.Date)
    {
        return WithValueConverter(new DateOnlyValueConverter(format));
    }
}