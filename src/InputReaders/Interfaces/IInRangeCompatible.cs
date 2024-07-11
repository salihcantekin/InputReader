namespace InputReader.InputReaders.Interfaces;

public interface IInRangeCompatible<T>
{
    bool IsInRange(T fromValue, T toValue);
}
