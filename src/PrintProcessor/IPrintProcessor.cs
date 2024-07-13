using System.Collections.Generic;

namespace InputReader.PrintProcessor;

public interface IPrintProcessor
{
    void Print(string message);
    void Print(char chr);

    void PrintLine(string message);
    void PrintLine(char chr);

    void PrintAllowedValues(IEnumerable<string> allowedValues, bool isCaseInSensitive);
}
