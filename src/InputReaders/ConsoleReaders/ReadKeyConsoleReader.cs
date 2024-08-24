using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders.ConsoleReaders;

internal class ReadKeyConsoleReader : BaseConsoleReader, IInputReaderBase
{
    public object Read()
    {
        return InternalRead(readLine: false, readKey: true);
    }
}