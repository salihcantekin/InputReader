using InputReader.InputReaders.BaseInputReaders;

namespace InputReader;

public sealed class YesNoInputReader : BaseInputReader<char?, YesNoInputValue>
{
    public static YesNoInputReader YesNo(string message = null) => new(message);

    public YesNoInputReader(string message) : base(message)
    {
        WithAllowedValues(caseInsensitive: true, Constants.Chars.YesLower, Constants.Chars.NoLower);
    }

    public YesNoInputReader() : this(null) { }

}