using System;
using System.Globalization;

namespace InputReader.Converters.CustomConverters;

public readonly struct CustomDateOnly : IComparable<CustomDateOnly>, IEquatable<CustomDateOnly>
{
    private readonly DateTime dateTime;

    private CustomDateOnly(int year, int month, int day)
    {
        dateTime = new DateTime(year, month, day);
    }

    public int Year => dateTime.Year;
    public int Month => dateTime.Month;
    public int Day => dateTime.Day;

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

    public int CompareTo(CustomDateOnly other) => dateTime.CompareTo(other.dateTime);
    public bool Equals(CustomDateOnly other) => dateTime.Equals(other.dateTime);
    public override bool Equals(object obj) => obj is CustomDateOnly other && Equals(other);
    public override int GetHashCode() => dateTime.GetHashCode();
    public static bool operator ==(CustomDateOnly left, CustomDateOnly right) => left.Equals(right);
    public static bool operator !=(CustomDateOnly left, CustomDateOnly right) => !left.Equals(right);
    public override string ToString() => dateTime.ToString("yyyy-MM-dd");
}