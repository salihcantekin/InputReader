using InputReader.InputReaders.Interfaces;
using System;
using System.Globalization;

namespace InputReader.Converters.CustomConverters;

public readonly struct CustomDateOnly(int year, int month, int day) : IComparable<CustomDateOnly>, IEquatable<CustomDateOnly>, IInRangeCompatible<CustomDateOnly>
{
    internal readonly DateTime DateTime { get; } = new DateTime(year, month, day);

    public static CustomDateOnly From(DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day);
    public static CustomDateOnly From(int year, int month, int day) => new(year, month, day);

    public int Year => DateTime.Year;
    public int Month => DateTime.Month;
    public int Day => DateTime.Day;

    public static bool TryParseExact(string s, string format, out CustomDateOnly? result)
    {
        return TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
    }

    public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style,
        out CustomDateOnly? result)
    {
        if (DateTime.TryParseExact(s, format, provider, style, out var dateTime))
        {
            result = new CustomDateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
            return true;
        }

        result = default;
        return false;
    }

    public bool IsInRange(CustomDateOnly fromValue, CustomDateOnly toValue)
    {
        return this >= fromValue && this <= toValue;
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