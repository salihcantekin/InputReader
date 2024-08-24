using InputReader.InputReaders.BaseInputReaders;

namespace InputReader.InputReaders;

public sealed class StringInputReader : BaseInputReader<string, StringInputValue>
{
    public static StringInputReader Str(string message = null) => new(message);

    public StringInputReader(string message) : base(message)
    {
        this.With(builder =>
        {
            builder.WithPreValidator((result) =>
            {
                // to avoid null or empty string
                return result?.Length > 0 && !string.IsNullOrWhiteSpace(result);
            });
        });
    }

    public StringInputReader() : this(null) { }
}
