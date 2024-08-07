using InputReader.Converters;
using InputReader.InputReaders.ConsoleReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue;
using InputReader.InputReaders.Queue.QueueItems;
using InputReader.PrintProcessor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InputReader.InputReaders.BaseInputReaders;

public abstract partial class BaseInputReader<TInputType, TInputValueType>
: IInputReader<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private IInputReaderBase consoleReader;
    protected IPrintProcessor PrintProcessor;

    internal SortedList<int, IQueueItem> queueItems;

    public BaseInputReader()
    {
        valueConverter = new DefaultValueConverter<TInputType>();
        PrintProcessor = new DefaultPrintProcessor();
        consoleReader = new DefaultConsoleReader();

        queueItems = [];

        AddItemToQueue(new ConsoleReadQueueItem(consoleReader));
        AddItemToQueue(new ValueConverterQueueItem<TInputType>(valueConverter));
        AddItemToQueue(new CreateInstanceQueueItem(typeof(TInputValueType)));
    }

    public virtual TInputValueType Read()
    {
        QueueItemResult itemResult = null;
        IQueueItem item = null;

        for (int i = 0; i < queueItems.Count; i++)
        {
            item = queueItems.Values[i];
            itemResult = item.Execute(itemResult);

            if (itemResult?.IsFailed == true)
            {
                break;
            }
        }

        // in case of intance of TInputValueType NOT created yet (CreateInstanceQueueItem didn't worked)
        if (itemResult.GetOutputParam(Constants.Queue.Params.InputValue) is not TInputValueType inputValue)
        {
            object value = itemResult.GetOutputParam(Constants.Queue.Params.ConvertedValue);
            inputValue = Activator.CreateInstance(typeof(TInputValueType), value) as TInputValueType;
        }

        inputValue.IsValid = !itemResult.IsFailed;
        inputValue.FailReason = (item as IHasFailReason)?.FailReason ?? FailReason.UnKnown;

        iteractionDelegate?.Invoke(inputValue, PrintProcessor);

        return inputValue;
    }

    internal IInputReader<TInputType, TInputValueType> SetConsoleReader(IInputReaderBase reader)
    {
        consoleReader = reader;

        var queueItem = TryGetQueueItem<ConsoleReadQueueItem>();

        queueItem?.SetInputReader(reader);

        return this;
    }

    internal IInputReader<TInputType, TInputValueType> SetPrintProcessor(IPrintProcessor printProcessor)
    {
        PrintProcessor = printProcessor;
        var queueItem = TryGetQueueItem<ProcessPrintQueueItem<TInputType>>();

        queueItem?.SetPrintProcessor(printProcessor);

        return this;
    }



    internal void AddItemToQueue(IQueueItem item)
    {
        queueItems[item.Order] = item;
    }

    private T TryGetQueueItem<T>() where T : IQueueItem
    {
        T queueItem = (T)queueItems.Values.FirstOrDefault(queueItem => queueItem is T);

        return queueItem;
    }

    private T GetOrCreateQueueItem<T>(Func<T> action) where T : IQueueItem
    {
        var queueItem = queueItems.FirstOrDefault(queueItems => queueItems.Value is T);

        if (queueItems.ContainsKey(queueItem.Key)) // instance of T already created
            return (T)queueItem.Value;

        var item = action();
        queueItems[item.Order] = item;

        return item;
    }
}