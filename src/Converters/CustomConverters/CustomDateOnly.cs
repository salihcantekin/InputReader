using System;
using System.Globalization;

namespace InputReader.Converters.CustomConverters;

public readonly struct CustomDateOnly : IComparable<CustomDateOnly>, IEquatable<CustomDateOnly>
{
    internal readonly DateTime DateTime { get; }

    private CustomDateOnly(int year, int month, int day)
    {
        DateTime = new DateTime(year, month, day);
    }

    public int Year => DateTime.Year;
    public int Month => DateTime.Month;
    public int Day => DateTime.Day;

    public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style,
        out CustomDateOnly result)
    {
        if (DateTime.TryParseExact(s, format, provider, style, out var dateTime))
        {
            result = new CustomDateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
            return true;
        }

        result = default;
        return false;
    }

    public int CompareTo(CustomDateOnly other) => DateTime.CompareTo(other.DateTime);
    public bool Equals(CustomDateOnly other) => DateTime.Equals(other.DateTime);
    public override bool Equals(object obj) => obj is CustomDateOnly other && Equals(other);
    public override int GetHashCode() => DateTime.GetHashCode();
    public override string ToString() => DateTime.ToString("yyyy-MM-dd");

    public static bool operator ==(CustomDateOnly left, CustomDateOnly right) => left.Equals(right);
    public static bool operator !=(CustomDateOnly left, CustomDateOnly right) => !left.Equals(right);

    public static bool operator >=(CustomDateOnly left, CustomDateOnly right) => left.DateTime >= right.DateTime;

    public static bool operator <=(CustomDateOnly left, CustomDateOnly right) => left.DateTime <= right.DateTime;


    public static implicit operator DateTime(CustomDateOnly customDateOnly) => customDateOnly.DateTime;
    public static implicit operator CustomDateOnly(DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day);
}