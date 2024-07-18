using InputReader.InputReaders.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputReader.InputReaders.BaseInputReaders;
// To Manage Message Functionality
public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>, IPreValidatable<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    internal string generatedMessage;
    private string errorMessage;

    public BaseInputReader(string message)
        : this()
    {
        if (!string.IsNullOrEmpty(message))
            WithMessage(message);
    }

    public virtual IInputReader<TInputType, TInputValueType> WithMessage(string message)
    {
        generatedMessage = message;

        var printQueueItem = new ProcessPrintQueueItem(PrintProcessor, allowedValueProcessor, generatedMessage);
        AddItemToQueue(printQueueItem);

        return this;
    }

    public virtual IInputReader<TInputType, TInputValueType> WithErrorMessage(string message)
    {
        errorMessage = message;
        iterationAction = (result, printProcessor) =>
        {
            if (!result.IsValid)
                printProcessor.PrintError(errorMessage);
        };

        return this;
    }
}
