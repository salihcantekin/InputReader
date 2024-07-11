using InputReader.InputReaders;

namespace InputReader;

public sealed class YesNoInputReader : BaseInputReader<char?, YesNoInputValue>
{
    public static YesNoInputReader YesNo(string message = null) => new(message);

    public YesNoInputReader(string message) : base(message)
    {
        WithAllowedValues(caseInsensitive: true, 'y', 'n');
    }

    public YesNoInputReader() : this(null)
    {
    }

}