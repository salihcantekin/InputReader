using System;
using System.Collections.Generic;

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

    public void PrintAllowedValues(IEnumerable<string> allowedValues, bool isCaseInSensitive)
    {
        var allowedMessage = $"({string.Join(',', allowedValues)}{(isCaseInSensitive ? '°' : '\0')}) ";
        Print(allowedMessage);
    }

    
}