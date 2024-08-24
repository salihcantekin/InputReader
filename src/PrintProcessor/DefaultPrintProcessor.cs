using System;
using System.Collections.Generic;
using System.Linq;

namespace InputReader.PrintProcessor;

public class DefaultPrintProcessor : IPrintProcessor
{
    public virtual void Print(string message)
    {
        Console.Write(message);
    }

    public virtual void Print(char chr)
    {
        Console.Write(chr);
    }

    public virtual void PrintLine(char chr)
    {
        Console.WriteLine(chr);
    }

    public virtual void PrintLine(string message)
    {
        Console.WriteLine(message);
    }

    public virtual void PrintError(string message)
    {
        Console.Error.WriteLine(message);
    }

    public virtual void PrintAllowedValues<TInputType>(IEnumerable<string> allowedValues, IEnumerable<(TInputType From, TInputType To)> inRangeAllowedValues, bool? isCaseInSensitive)
    {
        if (allowedValues is not null && inRangeAllowedValues is not null)
        {
            var ranges = inRangeAllowedValues?.Select(i => $"[{i.From}..{i.To}]");
            allowedValues = allowedValues.Concat(ranges);
        }

        if (allowedValues is null)
            return;

        var allowedMessage = $"({string.Join(',', allowedValues)}{(isCaseInSensitive == true ? '°' : '\0')}) ";

        Print(allowedMessage);
    }
}