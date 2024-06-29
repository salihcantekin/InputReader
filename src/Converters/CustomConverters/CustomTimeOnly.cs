using System;
using System.Globalization;

namespace InputReader.Converters.CustomConverters;

public readonly struct CustomTimeOnly : IComparable<CustomTimeOnly>, IEquatable<CustomTimeOnly>
{
    internal readonly TimeSpan TimeSpan { get; }
    
    public static CustomTimeOnly From(TimeSpan timeSpan) => new(timeSpan);
    public static CustomTimeOnly From(int hour, int minute = 0, int second = 0) => new(hour, minute, second);

    private CustomTimeOnly(TimeSpan timeSpan)        
    {
        this.TimeSpan = timeSpan;
    }

    public CustomTimeOnly(int hour, int minute = 0, int second = 0) 
        : this(new TimeSpan(hour, minute, second)) 
    {

    }

    public int Hour => TimeSpan.Hours;
    public int Minute => TimeSpan.Minutes;
    public int Second => TimeSpan.Seconds;
    public int Millisecond => TimeSpan.Milliseconds;

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

    public int CompareTo(CustomTimeOnly other) => TimeSpan.CompareTo(other.TimeSpan);
    public bool Equals(CustomTimeOnly other) => TimeSpan.Equals(other.TimeSpan);
    public override bool Equals(object obj) => obj is CustomTimeOnly other && Equals(other);
    public override int GetHashCode() => TimeSpan.GetHashCode();
    public override string ToString() => TimeSpan.ToString(@"hh\:mm\:ss");

    public static bool operator ==(CustomTimeOnly left, CustomTimeOnly right) => left.Equals(right);
    public static bool operator !=(CustomTimeOnly left, CustomTimeOnly right) => !left.Equals(right);

    public static bool operator >=(CustomTimeOnly left, CustomTimeOnly right) => left.TimeSpan >= right.TimeSpan;
    public static bool operator <=(CustomTimeOnly left, CustomTimeOnly right) => left.TimeSpan <= right.TimeSpan;

    public static implicit operator TimeSpan(CustomTimeOnly customTimeOnly) => customTimeOnly.TimeSpan;
    public static implicit operator CustomTimeOnly(TimeSpan timeSpan) => new(timeSpan);
}