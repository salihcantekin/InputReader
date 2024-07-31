using System.Collections.Generic;

namespace InputReader.PrintProcessor;

public interface IPrintProcessor
{
    void Print(string message);
    void Print(char chr);

    void PrintLine(string message);
    void PrintLine(char chr);

    void PrintError(string message);

    void PrintAllowedValues<TInputType>(IEnumerable<string> allowedValues, IEnumerable<(TInputType From, TInputType To)> inRangeAllowedValues, bool? isCaseInSensitive);
}
