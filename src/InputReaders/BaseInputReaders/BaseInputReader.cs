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
    private Action<TInputValueType, IPrintProcessor> iterationAction;

    internal SortedList<int, IQueueItem> queueItems;


    public BaseInputReader()
    {
        WithValueConverter(new DefaultValueConverter<TInputType>());
        PrintProcessor = new DefaultPrintProcessor();
        consoleReader = new DefaultConsoleReader();

        queueItems = [];

        var consoleReadLineQueueItem = new ConsoleReadLineQueueItem(consoleReader);
        var valueConverterQueueItem = new ValueConverterQueueItem<TInputType>(valueConverter);

        AddItemToQueue(consoleReadLineQueueItem);
        AddItemToQueue(valueConverterQueueItem);
    }

    internal void AddItemToQueue(IQueueItem item)
    {
        queueItems[item.Order] = item;
    }


    internal void SetConsoleReader(IInputReaderBase reader)
    {
        consoleReader = reader;
    }

    #region Read Methods

    // TODO: Refactor this method
    // UNDONE
    // MY_NEW_TODO
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

        var (success, value) = ReadGeneric();

        var result = Activator.CreateInstance(typeof(TInputValueType), success ? value : null) as TInputValueType;

        if (result is not null)
            result.IsValid = success;

        iterationAction?.Invoke(result, PrintProcessor);

        return result;
    }

    protected (bool success, TInputType result) ReadGeneric()
    {
        QueueItemResult previousItemResult = null;
        for (int i = 0; i < queueItems.Count; i++)
        {
            try
            {
                var item = queueItems.Values[i];
                previousItemResult = item.Execute(previousItemResult);

                if (previousItemResult?.IsFailed == true)
                    return default;
            }
            catch
            {
                return (false, default);
            }
        }

        return (true, (TInputType)previousItemResult.Result);
    }

    protected (bool success, TInputType result) ReadGeneric_()
    {
        try
        {
            ProcessPrint();

            var m = consoleReader.ReadLine();

            // preValidators
            if (AnyPreValidatorFailed(m))
                return default;

            // allowed values check
            if (AllowedValuesCheckRequired() && !IsAllowedValue(m))
                return default;

            var success = valueConverter.TryConvertFromString(m, out var value);

            if (!success)
                return default;

            return (true, value);
        }
        catch (ArgumentException)
        {
            return (false, default);
        }
    }

    public IInputReader<TInputType, TInputValueType> WithIteration(Action<TInputValueType, IPrintProcessor> action)
    {
        iterationAction = action;
        return this;
    }

    #endregion

    #region Pre-Build Methods

    #endregion







    public virtual IInputReader<TInputType, TInputValueType> WithDefaultValue(TInputType defaultValue)
    {

        return this;
    }

    private void ProcessPrint()
    {
        PrintProcessor.Print(generatedMessage);

        if (IsAllowedValuesEnabled())
            PrintProcessor.PrintAllowedValues(allowedValueProcessor.Values, allowedValueProcessor.IsCaseInSensitive);
    }

}