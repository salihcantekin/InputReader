using InputReader.AllowedValues;
using InputReader.InputReaders;
using InputReader.InputReaders.Interfaces;
using InputReader.Validators;
using System.Collections.Generic;
using System;

namespace InputReader;

public sealed class IntInputReader : BaseInputReader<int, IntInputValue>
{
    public static IntInputReader Int(string message = null) => new(message);

    public IntInputReader(string message) : base(message)
    {
        WithPreValidator(ValidatorBuilder.BuildIntInputPreValidator());
    }

    public IntInputReader() : this(null)
    {
    }
}