using InputReader.Converters;
using InputReader.InputReaders.BaseInputReaders;
using InputReader.InputReaders.ConsoleReaders;
using System;

namespace InputReader.InputReaders;

public sealed class ConsoleKeyInfoInputReader : BaseInputReader<ConsoleKeyInfo, ConsoleKeyInfoInputValue>
{
    public static ConsoleKeyInfoInputReader ConsoleKeyInfo(string message = null) => new(message);

    public ConsoleKeyInfoInputReader(string message) : base(message)
    {
        this.With(builder =>
        {
            builder.WithCustomConverter(new InternalValueConverter<ConsoleKeyInfo>());
            builder.WithConsoleReader(new ReadKeyConsoleReader());
        });

        WithIteration((_, processor) =>
        {
            processor.PrintLine(""); // Add a new line after the key in pressed
        });

    }

    public ConsoleKeyInfoInputReader() : this(null)
    {
    }
}
