using InputReader.Converters.CustomConverters;
using InputReader.InputReaders.Interfaces;
using System;

namespace InputReader;

public record IntInputValue(int? Value) : InputValue<int?>(Value), IInRangeCompatible<int?>
{
    public bool Is(int value) => value == Value;
    public bool IsZero() => Is(0);
    public bool IsOne() => Is(1);
    public bool IsTwo() => Is(2);
    public bool IsThree() => Is(3);
    public bool IsFour() => Is(4);
    public bool IsFive() => Is(5);

    public bool IsGreaterThan(int value) => Value > value;
    public bool IsLessThan(int value) => Value < value;
    public bool IsGreaterThanOrEqualTo(int value) => Value >= value;
    public bool IsLessThanOrEqualTo(int value) => Value <= value;    
}