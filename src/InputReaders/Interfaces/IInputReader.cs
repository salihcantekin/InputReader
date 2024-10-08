﻿using InputReader.PrintProcessor;
using System;
using System.Collections.Generic;

namespace InputReader.InputReaders.Interfaces;

public interface IInputReader<TInputType, TInputValueType> : IInputReaderBase<TInputType, TInputValueType>,
                                                                   IInputReadUntil<TInputType, TInputValueType>
    where TInputValueType : InputValue<TInputType>
{
    IInputReader<TInputType, TInputValueType> WithMessage(string message);

    IInputReader<TInputType, TInputValueType> WithErrorMessage();
    IInputReader<TInputType, TInputValueType> WithErrorMessage(string message);

    IInputReader<TInputType, TInputValueType> ClearAllowedValues();

    IInputReader<TInputType, TInputValueType> WithAllowedValues(bool caseInsensitive = true, string errorMessage = null, params TInputType[] allowedValues);

    IInputReader<TInputType, TInputValueType> WithAllowedValues(params TInputType[] allowedValues);

    IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<TInputType> allowedValues,
        bool caseInsensitive, string errorMessage);

    IInputReader<TInputType, TInputValueType> WithAllowedValues(IEnumerable<TInputType> allowedValues, bool caseInsensitive);


    IInputReader<TInputType, TInputValueType> WithIteration(Action<TInputValueType> action);
    IInputReader<TInputType, TInputValueType> WithIteration(Action<TInputValueType, IPrintProcessor> action);
}