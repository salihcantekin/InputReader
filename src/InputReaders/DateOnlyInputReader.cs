using InputReader.Converters.CustomConverters;

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
}   