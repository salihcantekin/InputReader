﻿using InputReader.AllowedValues;
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

namespace InputReader.InputReaders;

public abstract class
    BaseInputReader<TInputType, TCustomInputValueType> : IInputReader<TInputType, TCustomInputValueType>,
    IPreValidatable<TInputType, TCustomInputValueType>
    where TCustomInputValueType : InputValue<TInputType>
{
    private IInputReaderBase consoleReader;
    private IValueConverter<TInputType> valueConverter;
    private bool isPreBuildProcessed;

    private DefaultAllowedValueManager<string> allowedValueProcessor;
    internal readonly IPrintProcessor printProcessor;

    private string generatedMessage;

    private readonly HashSet<IPreValidator> preValidators = [];
    private readonly HashSet<IPostValidator<TInputType>> postValidators = [];
    private Action<TCustomInputValueType, IPrintProcessor> iterationAction;

    public FrozenSet<IPreValidator> PreValidators => preValidators.ToFrozenSet();
    public FrozenSet<IPostValidator<TInputType>> PostValidators => postValidators.ToFrozenSet();

    public BaseInputReader()
    {
        valueConverter = new DefaultValueConverter<TInputType>();
        printProcessor = new DefaultPrintProcessor();
        consoleReader = new DefaultConsoleReader();
    }

    public BaseInputReader(string message)
        : this()
    {
        if (!string.IsNullOrEmpty(message))
            WithMessage(message);
    }

    #region Read Methods

    // TODO: Refactor this method
    // UNDONE
    // MY_NEW_TODO
    public virtual TCustomInputValueType Read()
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

        var result = Activator.CreateInstance(typeof(TCustomInputValueType), success ? value : null) as TCustomInputValueType;

        if (result is not null)
            result.IsValid = success;

        iterationAction?.Invoke(result, printProcessor);
        
        return result;
    }

    protected (bool success, TInputType result) ReadGeneric()
    {
        try
        {
            ProcessPreBuild();
            ProcessPrint();

            var m = consoleReader.ReadLine();

            // preValidators
            if (preValidators.Count > 0 && preValidators.Any(v => !v.IsValid(m)))
                return default;

            // allowed values check
            if (!allowedValueProcessor.IsAllowedValue(m))
                return default;

            var success = valueConverter.TryConvertFromString(m, out var value);

            if (!success)
                return default;

            // postValidators
            if (postValidators.Count > 0 && postValidators.Any(v => !v.IsValid(value)))
                return default;

            return (true, value);
        }
        catch (ArgumentException)
        {
            return (false, default);
        }
    }
    
    public IInputReader<TInputType, TCustomInputValueType> WithIteration(Action<TCustomInputValueType, IPrintProcessor> action)
    {
        iterationAction = action;
        return this;
    }

    #endregion

    public virtual IInputReader<TInputType, TCustomInputValueType> WithMessage(string message)
    {
        generatedMessage = message;
        return this;
    }

    #region Pre-Post Validators

    public IInputReader<TInputType, TCustomInputValueType> WithPreValidator<TPreValidator>(TPreValidator validator)
        where TPreValidator : IPreValidator
    {
        preValidators.Add(validator);
        return this;
    }

    public void AddPostValidator<TPostValidator>(TPostValidator validator)
        where TPostValidator : IPostValidator<TInputType>
    {
        postValidators.Add(validator);
    }

    public IInputReader<TInputType, TCustomInputValueType> WithPreValidator(Func<string, bool> validatorFunc)
    {
        preValidators.Add(ValidatorBuilder.SetCustomPreValidator(validatorFunc));
        return this;
    }

    #endregion

    #region WithAllowedValues Methods

    public IInputReader<TInputType, TCustomInputValueType> ClearAllowedValues()
    {
        allowedValueProcessor.ClearAllowedValues();
        return this;
    }

    public IInputReader<TInputType, TCustomInputValueType> WithAllowedValues(bool caseInsensitive = true, params TInputType[] allowedValues)
    {
        return WithAllowedValues(allowedValues.Select(i => i.ToString()), caseInsensitive);
    }

    public IInputReader<TInputType, TCustomInputValueType> WithAllowedValues(params TInputType[] allowedValues)
    {
        return WithAllowedValues(allowedValues.Select(i => i.ToString()), false);
    }

    public IInputReader<TInputType, TCustomInputValueType> WithAllowedValues(IEnumerable<TInputType> allowedValues,
        bool caseInsensitive = true)
    {
        return WithAllowedValues(allowedValues.Select(i => i.ToString()), caseInsensitive);
    }

    public IInputReader<TInputType, TCustomInputValueType> WithAllowedValues(IEnumerable<string> allowedValues,
        bool caseInsensitive = true)
    {
        WithAllowedValues(allowedValues);

        if (caseInsensitive)
            allowedValueProcessor.SetEqualityComparer(StringComparer.OrdinalIgnoreCase);

        return this;
    }

    public IInputReader<TInputType, TCustomInputValueType> WithAllowedValues(IEnumerable<string> allowedValues)
    {
        if (allowedValues is null)
            throw new ArgumentNullException(nameof(allowedValues));

        PreBuildAllowedValues();

        allowedValueProcessor.AddAllowedValues(allowedValues);

        return this;
    }

    #endregion

    #region Converter Methods

    public IInputReader<TInputType, TCustomInputValueType> WithValueConverter(
        IValueConverter<TInputType> converter)
    {
        valueConverter = converter;

        return this;
    }

    public IInputReader<TInputType, TCustomInputValueType> WithValueConverter(
        Func<string, TInputType> convertFunc)
    {
        (valueConverter as DefaultValueConverter<TInputType>).InternalFunc = convertFunc;

        return this;
    }

    #endregion

    #region Pre-Build Methods

    private void ProcessPreBuild()
    {
        if (isPreBuildProcessed)
            return;

        PreBuildAllowedValues();

        isPreBuildProcessed = true;
    }


    private void PreBuildAllowedValues()
    {
        allowedValueProcessor ??= new DefaultAllowedValueManager<string>();
    }

    #endregion

    private void ProcessPrint()
    {
        printProcessor.Print(generatedMessage);

        if (allowedValueProcessor.IsEnabled)
            printProcessor.PrintAllowedValues(allowedValueProcessor.Values, allowedValueProcessor.IsCaseInSensitive);
    }

    #region Internals

    internal void SetConsoleReader(IInputReaderBase reader)
    {
        consoleReader = reader;
    }

    #endregion
}