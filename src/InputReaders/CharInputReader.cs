using InputReader.InputReaders;
using InputReader.Validators;

namespace InputReader;

public class CharInputReader : BaseInputReader<char, CharInputValue>
{
    public static CharInputReader Chr(string message = null) => new(message);

    public CharInputReader(string message) : base(message)
    {
        WithPreValidator(ValidatorBuilder.BuildCharInputValidator());
    }

    public CharInputReader() : this(null)
    {
    }
}