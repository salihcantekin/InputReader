using System;

namespace InputReader;

public record InputValue<T>(T Value)
{
    public T Value { get; protected set; } = Value;

    public bool IsValid { get; set; }

    public override string ToString() => Value?.ToString();

    public static implicit operator T(InputValue<T> wrapper)
    {
        return wrapper.Value;
    }

    public static implicit operator InputValue<T>(T value)
    {
        return new InputValue<T>(value);
    }
}


