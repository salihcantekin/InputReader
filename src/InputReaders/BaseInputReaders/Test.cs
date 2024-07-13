using InputReader.AllowedValues;
using InputReader.InputReaders.Interfaces;
using InputReader.PrintProcessor;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace InputReader.InputReaders.BaseInputReaders;
internal class ProcessPrintQueueItem<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private readonly BaseInputReader<TInputType, TInputValueType> reader;
    private readonly NameValueCollection parameters;

    public ProcessPrintQueueItem(BaseInputReader<TInputType, TInputValueType> reader,
        NameValueCollection parameters)
    {
        this.reader = reader;
        this.parameters = parameters;
    }

    public void Execute()
    {
        var processor = reader.AllowedValueProcessor;
        reader.PrintProcessor.Print(reader.generatedMessage);
        if (reader.IsAllowedValuesEnabled())
            reader.PrintProcessor.PrintAllowedValues(processor.Values, processor.IsCaseInSensitive);
    }
}

internal class ReadQueue<TInputType>
{
    private Queue<TInputType> queue = new();

    public void Enqueue(TInputType item)
    {
        queue.Enqueue(item);
    }

    public TInputType Dequeue()
    {
        return queue.Dequeue();
    }

    public TInputType Peek()
    {
        return queue.Peek();
    }

    public bool IsEmpty()
    {
        return queue.Count == 0;
    }
}
{
}
