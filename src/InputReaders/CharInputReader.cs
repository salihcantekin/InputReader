using InputReader.InputReaders.BaseInputReaders;
using InputReader.Validators;

namespace InputReader.InputReaders;

public class CharInputReader : BaseInputReader<char?, CharInputValue>
{
    public static CharInputReader Chr(string message = null) => new(message);

    public CharInputReader(string message) : base(message)
    {
        this.With(builder =>
        {
            builder.WithPreValidator(ValidatorBuilder.BuildCharInputValidator());
        });
    }

    public CharInputReader() : this(null)
    {
    }
}