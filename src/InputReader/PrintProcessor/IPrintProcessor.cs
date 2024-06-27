using System.Collections.Generic;

namespace InputReader.PrintProcessor;

public interface IPrintProcessor
{
    void Print(string message);

    void PrintLine(string message);

    void PrintAllowedValues(IEnumerable<string> allowedValues, bool isCaseInSensitive);
}
