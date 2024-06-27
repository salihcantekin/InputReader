using System;
using System.Globalization;

namespace InputReader.Converters.CustomConverters;

public readonly struct CustomTimeOnly : IComparable<CustomTimeOnly>, IEquatable<CustomTimeOnly>
{
    private readonly TimeSpan timeSpan;
    
    private CustomTimeOnly(TimeSpan timeSpan)        
    {
        this.timeSpan = timeSpan;
    }

    public int Hour => timeSpan.Hours;
    public int Minute => timeSpan.Minutes;
    public int Second => timeSpan.Seconds;
    public int Millisecond => timeSpan.Milliseconds;

    public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out CustomTimeOnly result)
    {
        if (DateTime.TryParseExact(s, format, provider, style, out var dateTime))
        {
            result = new CustomTimeOnly(dateTime.TimeOfDay);
            return true;
        }
        result = default;
        return false;
    }

    public int CompareTo(CustomTimeOnly other) => timeSpan.CompareTo(other.timeSpan);
    public bool Equals(CustomTimeOnly other) => timeSpan.Equals(other.timeSpan);
    public override bool Equals(object obj) => obj is CustomTimeOnly other && Equals(other);
    public override int GetHashCode() => timeSpan.GetHashCode();
    public static bool operator ==(CustomTimeOnly left, CustomTimeOnly right) => left.Equals(right);
    public static bool operator !=(CustomTimeOnly left, CustomTimeOnly right) => !left.Equals(right);
    public override string ToString() => timeSpan.ToString(@"hh\:mm\:ss");
}