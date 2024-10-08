﻿using InputReader.InputReaders;

namespace InputReader;

public sealed class Input
{
    public static StringInputReader String(string message = null) => new(message);
    public static IntInputReader Int(string message = null) => new(message);
    public static CharInputReader Char(string message = null) => new(message);
    public static YesNoInputReader YesNo(string message = null) => new(message);
    public static DateOnlyInputReader DateOnly(string message = null, string format = Constants.Format.DateFormat) => new(message, format);
    public static TimeOnlyInputReader TimeOnly(string message = null, string format = Constants.Format.TimeFormat) => new(message, format);
    public static PasswordInputReader Password(string message = null, char passwordChar = PasswordInputReader.DefaultPasswordChar) => new(message, passwordChar);

    public static ConsoleKeyInfoInputReader Key(string message = null) => new(message);
}
