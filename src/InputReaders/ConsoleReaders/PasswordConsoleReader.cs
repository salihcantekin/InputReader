using InputReader.PrintProcessor;
using System;
using System.Security;
using System.Text;

namespace InputReader.InputReaders.ConsoleReaders;

internal sealed class PasswordConsoleReader(char passwordChar, IPrintProcessor printProcessor) : DefaultConsoleReader
{
    public override string ReadLine()
    {
        var password = new StringBuilder();
        ConsoleKeyInfo key;

        do
        {
            key = ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password.Append(key.KeyChar);
                printProcessor.Print(passwordChar);
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    printProcessor.Print(Constants.Chars.DoubleBackspace); // Remove the last character from the console
                }
            }

        } while (key.Key != ConsoleKey.Enter);

        printProcessor.PrintLine(string.Empty);
        
        return password.ToString();
    }
}
