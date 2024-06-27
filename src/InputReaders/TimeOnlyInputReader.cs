using InputReader.Converters.CustomConverters;

namespace InputReader.InputReaders;

public sealed class TimeOnlyInputReader : BaseInputReader<CustomTimeOnly, TimeOnlyInputValue>
{
    public static TimeOnlyInputReader TimeOnly(string message = null) => new(message);

    public TimeOnlyInputReader(string message) : base(message)
    {
    }

    public TimeOnlyInputReader()
    {
    }
}