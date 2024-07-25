using System;

namespace InputReader;

public record InputValue<T>(T Value)
{
    public T Value { get; protected set; } = Value;

    public virtual bool IsValid { get; set; }

    public override string ToString() => Value?.ToString() ?? string.Empty;

    // generate implicit operators but nullable
    public static implicit operator T(InputValue<T> wrapper)
    {
        return wrapper.Value;
    }

    public static implicit operator InputValue<T>(T value)
    {
        return new InputValue<T>(value);
    }


    //public static implicit operator T(InputValue<T?> wrapper)
    //{
    //    return wrapper.Value;
    //}

    //public static implicit operator InputValue<T>(T? value)
    //{
    //    return new InputValue<T>(value);
    //}

    //public static implicit operator
}


