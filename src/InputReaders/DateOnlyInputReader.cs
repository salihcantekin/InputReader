using InputReader.Converters;
using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.BaseInputReaders;
using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders;

public sealed class DateOnlyInputReader : BaseInputReader<CustomDateOnly?, DateOnlyInputValue>
{
    public static DateOnlyInputReader DateOnly(string message = null, string format = Constants.Format.DateFormat) => new(message, format);

    public DateOnlyInputReader(string message, string format = Constants.Format.DateFormat) : base(message)
    {
        this.With(builder =>
        {
            builder.WithCustomConverter(new DateOnlyValueConverter(format));
        });
    }

    public DateOnlyInputReader() : this(null)
    {
    }
}
