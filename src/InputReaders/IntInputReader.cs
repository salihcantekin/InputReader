using InputReader.InputReaders.BaseInputReaders;
using InputReader.Validators;

namespace InputReader.InputReaders;

public sealed class IntInputReader : BaseInputReader<int?, IntInputValue>
{
    public static IntInputReader Int(string message = null) => new(message);

    public IntInputReader() : this(null) { }

    public IntInputReader(string message) : base(message)
    {
        SetPreValidator(ValidatorBuilder.BuildIntInputPreValidator());
    }
}