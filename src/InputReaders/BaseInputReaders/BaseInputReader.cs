using InputReader.Converters;
using InputReader.InputReaders.ConsoleReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue;
using InputReader.InputReaders.Queue.QueueItems;
using InputReader.PrintProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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

        AddItemToQueue(new ConsoleReadLineQueueItem(consoleReader));
        AddItemToQueue(new ValueConverterQueueItem<TInputType>(valueConverter));
        AddItemToQueue(new CreateInstanceQueueItem(typeof(TInputValueType)));
    }

    public virtual TInputValueType Read()
    {
        /* ############## STEPS #############

          - Write Message (Opt)             -> (PrintProcessor)
          - Read RawValue                   -> (IInputReaderBase)
          - PreValidators

          - AllowedValue Check (Opt) (rawValue)
          - Use ValueConverter.Convert (ValueConverter)
          - AllowedValue Check (Opt) (Converted Type) (OPT)
         */

        QueueItemResult previousItemResult = null;
        IQueueItem item = null;
        for (int i = 0; i < queueItems.Count; i++)
        {
            item = queueItems.Values[i];
            previousItemResult = item.Execute(previousItemResult);

            if (previousItemResult?.IsFailed == true)
            {
                break;
            }
        }

        // intance of TInputValueType NOT created yet (CreateInstanceQueueItem didn't worked)
        if (previousItemResult.GetOutputParam(Constants.Queue.Params.InputValue) is not TInputValueType inputValue)
        {
            object value = previousItemResult.GetOutputParam(Constants.Queue.Params.ConvertedValue);
            inputValue = Activator.CreateInstance(typeof(TInputValueType), value) as TInputValueType;
        }

        inputValue.IsValid = !previousItemResult.IsFailed;
        inputValue.FailReason = item is IHasFailReason failReason 
                                 ? failReason.FailReason 
                                 : FailReason.UnKnown;

        iteractionDelegate?.Invoke(inputValue, PrintProcessor);

        return inputValue;
    }


    internal void AddItemToQueue(IQueueItem item)
    {
        queueItems[item.Order] = item;
    }

    internal IInputReader<TInputType, TInputValueType> SetConsoleReader(IInputReaderBase reader)
    {
        consoleReader = reader;

        var queueItem = GetOrCreateQueueItem(() => new ConsoleReadLineQueueItem(consoleReader));

        queueItem.SetInputReader(reader);

        return this;
    }

    internal IInputReader<TInputType, TInputValueType> SetPrintProcessor(IPrintProcessor printProcessor)
    {
        PrintProcessor = printProcessor;

        return this;
    }


    private T GetOrCreateQueueItem<T>(Func<T> action) where T : IQueueItem
    {
        var queueItem = queueItems.FirstOrDefault(queueItems => queueItems.Value is T);

        if (queueItems.ContainsKey(queueItem.Key)) // instance of T already created
            return (T)queueItem.Value;

        var item = action();
        queueItems[queueItem.Key] = item;

        return item;
    }
}