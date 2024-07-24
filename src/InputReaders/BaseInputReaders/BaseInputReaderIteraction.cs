using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue.QueueItems;
using InputReader.PrintProcessor;
using System;

namespace InputReader.InputReaders.BaseInputReaders;
public abstract partial class BaseInputReader<TInputType, TInputValueType>
    : IInputReader<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private delegate void IteractionDelegate(TInputValueType result, IPrintProcessor printProcessor);
    private IteractionDelegate iteractionDelegate;

    public IInputReader<TInputType, TInputValueType> WithIteration(Action<TInputValueType, IPrintProcessor> action)
    {
        iteractionDelegate += (result, printProcessor) => action(result, printProcessor);

        return this;
    }
}
