namespace InputReader;

public class Constants
{
    public static class Format
    {
        public const string DateFormat = "yyyy-MM-dd";
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string TimeFormat = "HH:mm:ss";
    }

    public static class Chars
    {
        public const char Asterisk = '*';
        public const char YesLower = 'y';
        public const char YesUpper = 'Y';
        public const char NoLower = 'n';
        public const char NoUpper = 'N';
        public const char Space = ' ';
        public const char Tab = '\t';
        public const char NoChar = '\0';

        internal const char Backspace = '\b';
        internal const string DoubleBackspace = "\b \b";
    }
}
