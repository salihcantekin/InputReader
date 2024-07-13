using InputReader.InputReaders.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputReader.InputReaders.ConsoleReaders;

internal class DefaultConsoleReader : IInputReaderBase
{
    public virtual ConsoleKeyInfo ReadKey()
    {
        return Console.ReadKey();
    }

    public virtual ConsoleKeyInfo ReadKey(bool intercept = false)
    {
        return Console.ReadKey(intercept);
    }

    public virtual string ReadLine()
    {
        return Console.ReadLine();
    }
}
