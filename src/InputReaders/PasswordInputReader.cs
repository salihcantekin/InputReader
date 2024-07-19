﻿using InputReader.InputReaders.BaseInputReaders;
using InputReader.InputReaders.ConsoleReaders;
using InputReader.InputValues;

namespace InputReader.InputReaders;

public sealed class PasswordInputReader : BaseInputReader<string, PasswordInputValue>
{
    public const char DefaultPasswordChar = Constants.Chars.Asterisk;
    public static PasswordInputReader Password(string message = null, char passwordChar = DefaultPasswordChar) => new(message, passwordChar);

    public PasswordInputReader(string message, char passwordChar = DefaultPasswordChar) : base(message)
    {
        SetConsoleReader(new PasswordConsoleReader(passwordChar, PrintProcessor));
    }

    public PasswordInputReader() : base()
    {
    }
}