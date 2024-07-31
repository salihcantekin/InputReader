using System;
using System.Collections.Generic;
using System.Linq;

namespace InputReader.PrintProcessor;

public class DefaultPrintProcessor : IPrintProcessor
{
    public void Print(string message)
    {
        Console.Write(message);
    }

    public void Print(char chr)
    {
        Console.Write(chr);
    }

    public void PrintLine(char chr)
    {
        Console.WriteLine(chr);
    }

    public void PrintLine(string message)
    {
        Console.WriteLine(message);
    }

    public void PrintError(string message)
    {
        Console.Error.WriteLine(message);
    }

    public void PrintAllowedValues<TInputType>(IEnumerable<string> allowedValues, IEnumerable<(TInputType From, TInputType To)> inRangeAllowedValues, bool? isCaseInSensitive)
    {
        var ranges = inRangeAllowedValues.Select(i => $"[{i.From}..{i.To}]");
        allowedValues = allowedValues?.Concat(ranges) ?? ranges;

        var allowedMessage = $"({string.Join(',', allowedValues)}{(isCaseInSensitive == true ? '°' : '\0')}) ";

        Print(allowedMessage);
    }
}