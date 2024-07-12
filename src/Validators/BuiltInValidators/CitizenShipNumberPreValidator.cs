using System;
using System.Linq;
using InputReader.Validators.BuiltInValidators.Internals;

namespace InputReader.Validators.BuiltInValidators;

public class CitizenShipNumberPreValidator : IPreValidator
{
    public bool IsValid(string value)
    {
        if (!Int64.TryParse(value, out _))
            return false;

        var v = new InternalValueValidator<string>
        {
            ValidatorFunc = s => IsValidTCKN(s)
        };

        return v.IsValid(value);
    }

    public static bool IsValidTCKN(string tckn)
    {
        if (tckn.Length != 11 || !tckn.All(char.IsDigit))
        {
            return false;
        }

        if (tckn[0] == '0')
        {
            return false;
        }

        int[] digits = tckn.Select(digit => int.Parse(digit.ToString())).ToArray();
        int sumOfFirst10 = digits.Take(10).Sum();
        if (sumOfFirst10 % 10 != digits[10])
        {
            return false;
        }

        int sumOdd = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
        int sumEven = digits[1] + digits[3] + digits[5] + digits[7];
        int checkDigit1 = ((sumOdd * 7) - sumEven) % 10;
        if (checkDigit1 != digits[9])
        {
            return false;
        }

        return true;
    }
}