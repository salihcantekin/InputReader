using InputReader.AllowedValues;
using InputReader.Converters;
using InputReader.InputReaders.ConsoleReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.PrintProcessor;
using InputReader.Validators;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace InputReader.InputReaders.BaseInputReaders;







public abstract partial class BaseInputReader<TInputType, TInputValueType>
: IInputReader<TInputType, TInputValueType>, IPreValidatable<TInputType, TInputValueType>
where TInputValueType : InputValue<TInputType>
{
    private IInputReaderBase consoleReader;

    internal readonly IPrintProcessor PrintProcessor;

    private Action<TInputValueType, IPrintProcessor> iterationAction;



    public BaseInputReader()
    {
        WithValueConverter(new DefaultValueConverter<TInputType>());
        PrintProcessor = new DefaultPrintProcessor();
        consoleReader = new DefaultConsoleReader();
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

          - PostValidators
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

            // postValidators
            if (AnyPostValidatorFailed(value))
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
            PrintProcessor.PrintAllowedValues(AllowedValueProcessor.Values, AllowedValueProcessor.IsCaseInSensitive);
    }

}