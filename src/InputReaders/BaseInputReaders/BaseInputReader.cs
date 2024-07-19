using InputReader.Converters;
using InputReader.InputReaders.ConsoleReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.InputReaders.Queue;
using InputReader.InputReaders.Queue.QueueItems;
using InputReader.PrintProcessor;
using System;
using System.Collections.Generic;

namespace InputReader.InputReaders.BaseInputReaders;

public abstract partial class BaseInputReader<TInputType, TInputValueType>
: IInputReader<TInputType, TInputValueType>, IPreValidatable<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    private IInputReaderBase consoleReader;
    protected readonly IPrintProcessor PrintProcessor;

    internal SortedList<int, IQueueItem> queueItems;

    public BaseInputReader()
    {
        WithValueConverter(new DefaultValueConverter<TInputType>());
        PrintProcessor = new DefaultPrintProcessor();
        consoleReader = new DefaultConsoleReader();

        queueItems = [];

        AddItemToQueue(new ConsoleReadLineQueueItem(consoleReader));
        AddItemToQueue(new ValueConverterQueueItem<TInputType>(valueConverter));
        AddItemToQueue(new CreateInstanceQueueItem(typeof(TInputValueType)));
    }

    internal void AddItemToQueue(IQueueItem item)
    {
        queueItems[item.Order] = item;
    }

    internal void SetConsoleReader(IInputReaderBase reader)
    {
        consoleReader = reader;
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
        for (int i = 0; i < queueItems.Count; i++)
        {
            var item = queueItems.Values[i];
            previousItemResult = item.Execute(previousItemResult);

            if (previousItemResult?.IsFailed == true)
                break;
        }

        // intance of TInputValueType NOT created yet (CreateInstanceQueueItem didn't worked)
        if (previousItemResult.GetOutputParam("InputValue") is not TInputValueType inputValue)
        {
            object value = previousItemResult.GetOutputParam("converted_value");
            inputValue = Activator.CreateInstance(typeof(TInputValueType), value) as TInputValueType;
        }

        inputValue.IsValid = !previousItemResult.IsFailed;

        iteractionDelegate.Invoke(inputValue, PrintProcessor);

        return inputValue;
    }
}