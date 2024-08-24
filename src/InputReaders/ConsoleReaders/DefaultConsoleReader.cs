using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders.ConsoleReaders;

internal class DefaultConsoleReader : BaseConsoleReader, IInputReaderBase
{
    public object Read()
    {
        return InternalRead(readLine: true, false);
    }
}
