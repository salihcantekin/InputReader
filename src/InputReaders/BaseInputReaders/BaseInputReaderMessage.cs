using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue.QueueItems;

namespace InputReader.InputReaders.BaseInputReaders;

public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    public BaseInputReader(string message)
        : this()
    {
        if (!string.IsNullOrEmpty(message))
            WithMessage(message);
    }

    public virtual IInputReader<TInputType, TInputValueType> WithMessage(string message)
    {
        var printQueueItem = new ProcessPrintQueueItem(PrintProcessor, allowedValueProcessor, message);
        AddItemToQueue(printQueueItem);

        return this;
    }

    public virtual IInputReader<TInputType, TInputValueType> WithErrorMessage(string message)
    {
        WithIteration((result, printProcessor) =>
        {
            if (!result.IsValid)
                printProcessor.PrintError(message);
        });

        return this;
    }

    public virtual IInputReader<TInputType, TInputValueType> WithErrorMessage()
    {
        WithIteration((result, printProcessor) =>
        {
            if (!result.IsValid)
                printProcessor.PrintError(result.DefaultErrorMessage);
        });

        return this;
    }
}
