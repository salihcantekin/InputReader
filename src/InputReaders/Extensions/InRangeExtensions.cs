using InputReader.InputReaders.Interfaces;

namespace InputReader.InputReaders.Extensions;

public static class InRangeExtensions
{
    public static TInputValueType ReadUntilInRange<TInputType, TInputValueType>(
        this IInputReader<TInputType?, TInputValueType> reader,
        TInputType? from,
        TInputType? to)
        where TInputType : struct, IInRangeCompatible<TInputType>
        where TInputValueType : InputValue<TInputType?>
    {
        return reader.ReadUntil(input =>
        {
            // Ensure both input and range values are not null before calling IsInRange
            if (input.Value.HasValue && from.HasValue && to.HasValue)
            {
                return input.Value.Value.IsInRange(from.Value, to.Value);
            }

            // Handle cases where input or range values are null
            // Adjust this logic based on your requirements
            return false;
        });
    }
}
