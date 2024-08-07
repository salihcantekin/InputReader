using System;

namespace InputReader.InputReaders.ConsoleReaders;

internal abstract class BaseConsoleReader
{
    public object InternalRead(bool? readLine = true, bool? readKey = false)
    {
        if (readLine == true)
            return Console.ReadLine();

        if (readKey == true)
            return Console.ReadKey();

        throw new InvalidOperationException("No read method specified");
    }
}
