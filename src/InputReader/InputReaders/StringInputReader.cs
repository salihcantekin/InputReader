using InputReader.InputReaders;

namespace InputReader;

public sealed class StringInputReader : BaseInputReader<string, StringInputValue>
{
    public static StringInputReader Str(string message = null) => new(message);

    public StringInputReader(string message) : base(message)
    {
    }

    public StringInputReader() : base()
    {
    }
}