using InputReader.InputReaders.BaseInputReaders;

namespace InputReader.InputReaders;

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
