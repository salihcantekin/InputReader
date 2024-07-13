using InputReader.InputReaders;
using InputReader.InputReaders.ConsoleReaders;
using InputReader.InputValues;

namespace InputReader;

public sealed class PasswordInputReader : BaseInputReader<string, PasswordInputValue>
{
    public const char DefaultPasswordChar = '*';
    public static PasswordInputReader Password(string message = null, char passwordChar = DefaultPasswordChar) => new(message, passwordChar);

    public PasswordInputReader(string message, char passwordChar = DefaultPasswordChar) : base(message)
    {

        SetConsoleReader(new PasswordConsoleReader(passwordChar, printProcessor));
    }

    public PasswordInputReader() : base()
    {
    }
}