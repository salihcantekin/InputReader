namespace InputReader;

public record InputValue<T>
{
    internal InputValue(T Value)
    {
        this.Value = Value;
    }

    public T Value { get; protected set; }

    public bool IsValid { get; set; }

    protected internal virtual string DefaultErrorMessage => "Invalid value. Please try again.";

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


