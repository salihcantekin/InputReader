namespace InputReader;

public sealed class Input
{
    public static StringInputReader String(string message = null) => new(message);
    public static IntInputReader Int(string message = null) => new(message);
    public static CharInputReader Char(string message = null) => new(message);
    public static YesNoInputReader YesNo(string message = null) => new(message);
}
