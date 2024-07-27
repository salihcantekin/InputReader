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

    public static class Queue
    {
        internal class Params
        {
            internal const string Line = "Line";
            internal const string InputValue = "InputValue";
            internal const string ConvertedValue = "ConvertedValue";
        }
    }

    internal static class Message
    {
        internal const string DefaultErrorMessage = "Invalid value. Please try again.";
        internal const string YesNoErrorMessage = "Invalid value. Please enter either 'Y' or 'N'";
        internal const string InvalidValueFormat = "Invalid Value for {0}";
    }
}

internal static class StringExtensions
{
    internal static string Format(this string value, params object[] args)
    {
        return string.Format(value, args);
    }
}