namespace InputReader;

public enum FailReason
{
    UnKnown,
    PreValidation,
    PostValidation,
    ValueConversion,
    AllowedValues
}

public record InputValue<T>
{
    internal InputValue(T Value)
    {
        this.Value = Value;
    }

    public T Value { get; protected set; }

    public bool IsValid { get; set; }

    public FailReason FailReason { get; internal set; }

    protected internal virtual string DefaultErrorMessage => Constants.Message.DefaultErrorMessage;

    public sealed override string ToString() => Value?.ToString();

    public static implicit operator T(InputValue<T> wrapper)
    {
        return wrapper.Value;
    }

    public static implicit operator InputValue<T>(T value)
    {
        return new InputValue<T>(value);
    }
}


