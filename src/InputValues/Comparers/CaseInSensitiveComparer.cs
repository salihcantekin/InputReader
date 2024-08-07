using System;
using System.Collections.Generic;

namespace InputReader.InputValues.Comparers;

public sealed class CaseInSensitiveComparer<TInputValue> : IEqualityComparer<TInputValue>, ICaseInsensitiveComparer<TInputValue>
{
    public bool Equals(TInputValue x, TInputValue y)
    {
        return string.Equals(x.ToString(), y.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode(TInputValue obj)
    {
        return obj?.ToString().ToLowerInvariant().GetHashCode() ?? 0;
    }
}
