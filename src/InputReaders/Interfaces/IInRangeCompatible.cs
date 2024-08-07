namespace InputReader.InputReaders.Interfaces;

public interface IInRangeCompatible<TInputType>
{
    public static bool IsInRange(TInputType value, TInputType fromValue, TInputType toValue)
    {
        var isGreater = System.Collections.Generic.Comparer<TInputType>.Default.Compare(value, fromValue) > 0;
        var isLess = System.Collections.Generic.Comparer<TInputType>.Default.Compare(value, toValue) < 0;
        var isEqual = System.Collections.Generic.Comparer<TInputType>.Default.Compare(value, fromValue) == 0 || System.Collections.Generic.Comparer<TInputType>.Default.Compare(value, toValue) == 0;

        return isGreater && isLess || isEqual;
    }
}
