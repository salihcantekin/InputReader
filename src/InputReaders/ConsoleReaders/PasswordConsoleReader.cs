using InputReader.PrintProcessor;
using System;
using System.Security;

namespace InputReader.InputReaders.ConsoleReaders;

internal sealed class PasswordConsoleReader : DefaultConsoleReader
{
    private readonly char passwordChar;
    private readonly IPrintProcessor printProcessor;

    public PasswordConsoleReader(char passwordChar, IPrintProcessor printProcessor)
    {
        this.passwordChar = passwordChar;
        this.printProcessor = printProcessor;
    }

    public override string ReadLine()
    {
        var password = new SecureString();
        ConsoleKeyInfo key;

        do
        {
            key = ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password.AppendChar(key.KeyChar);
                printProcessor.Print(passwordChar);
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.RemoveAt(password.Length - 1);
                    printProcessor.Print(Constants.Chars.DoubleBackspace); // Remove the last character from the console
                }
            }

        } while (key.Key != ConsoleKey.Enter);

        printProcessor.PrintLine(string.Empty);

        return password.ToString();
    }
}
